using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ReportBuilder.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;

namespace ReportBuilder.Reports;

[Authorize(ReportBuilderPermissions.Reports.Viewer.Default)]
public class ReportExecutionAppService : ApplicationService, IReportExecutionAppService
{
    private readonly IReportRepository _reportRepository;
    private readonly ColumnResolver _columnResolver;
    private readonly IConfiguration _configuration;

    public ReportExecutionAppService(
        IReportRepository reportRepository,
        ColumnResolver columnResolver,
        IConfiguration configuration)
    {
        _reportRepository = reportRepository;
        _columnResolver = columnResolver;
        _configuration = configuration;
    }

    public async Task<ReportResultDto> ExecuteAsync(Guid reportId, ExecuteReportInput input)
    {
        var report = await _reportRepository.GetWithDetailsAsync(reportId)
            ?? throw new UserFriendlyException("Report not found.");

        if (!report.IsActive)
            throw new UserFriendlyException("This report is not active.");

        // Admins bypass per-report role restrictions
        var isAdmin = await AuthorizationService.IsGrantedAsync(ReportBuilderPermissions.Reports.Admin.Default);

        // Check report-level permission for non-admins
        var userRoles = CurrentUser.Roles ?? Array.Empty<string>();
        var hasAccess = isAdmin || !report.Permissions.Any() ||
                        report.Permissions.Any(p => userRoles.Contains(p.RoleName, StringComparer.OrdinalIgnoreCase));
        if (!hasAccess)
            throw new AbpAuthorizationException("You do not have access to this report.");

        // Resolve visible columns for this user
        var visibleColumns = _columnResolver.ResolveVisibleColumns(report, userRoles);
        var visibleFieldNames = visibleColumns.Select(c => c.FieldName).ToHashSet(StringComparer.OrdinalIgnoreCase);

        var connectionString = _configuration.GetConnectionString("ReadOnly")
            ?? _configuration.GetConnectionString("Default")
            ?? throw new UserFriendlyException("No database connection string configured.");

        await using var connection = new NpgsqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            var baseSql = WrapSql(report.SqlQuery, "__q");
            var parameters = BuildDapperParameters(input.Parameters);
            var commandTimeout = ReportConsts.QueryTimeoutSeconds;

            // Count query
            var countSql = $"SELECT COUNT(*) FROM ({report.SqlQuery}) AS __count";
            var totalCount = await connection.ExecuteScalarAsync<int>(countSql, parameters, commandTimeout: commandTimeout);

            // Paged data query
            var orderClause = !string.IsNullOrWhiteSpace(input.SortField) && visibleFieldNames.Contains(input.SortField)
                ? $" ORDER BY \"{input.SortField}\"{(input.SortDescending ? " DESC" : " ASC")}"
                : string.Empty;

            var maxRows = Math.Min(input.MaxResultCount, ReportConsts.MaxRowLimit);
            var dataSql = $"{baseSql}{orderClause} LIMIT {maxRows} OFFSET {input.SkipCount}";

            var rows = (await connection.QueryAsync(dataSql, parameters, commandTimeout: commandTimeout)).ToList();

            // Strip unauthorized columns
            var result = rows.Select(row =>
            {
                var dict = (IDictionary<string, object>)row;
                return visibleFieldNames
                    .Where(dict.ContainsKey)
                    .ToDictionary(f => f, f => (object?)dict[f], StringComparer.OrdinalIgnoreCase);
            }).ToList();

            return new ReportResultDto
            {
                Columns = ObjectMapper.Map<List<ReportColumn>, List<ReportColumnDto>>(visibleColumns),
                Rows = result,
                TotalCount = totalCount
            };
        }
        catch (NpgsqlException ex) when (ex.InnerException is TimeoutException || ex.Message.Contains("timeout", StringComparison.OrdinalIgnoreCase))
        {
            throw new UserFriendlyException("Report query timed out. Please refine your query or contact an administrator.");
        }
        catch (NpgsqlException ex)
        {
            throw new UserFriendlyException($"Database error while executing report: {ex.Message}");
        }
    }

    private static DynamicParameters BuildDapperParameters(Dictionary<string, object?> parameters)
    {
        var dp = new DynamicParameters();
        foreach (var (key, value) in parameters)
            dp.Add(key, value);
        return dp;
    }

    private static string WrapSql(string sql, string alias) => $"SELECT * FROM ({sql}) AS {alias}";
}
