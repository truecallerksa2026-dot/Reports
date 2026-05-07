using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ReportBuilder.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Guids;

namespace ReportBuilder.Reports;

[Authorize(ReportBuilderPermissions.Reports.Admin.Default)]
public class ReportDefinitionAppService : ApplicationService, IReportDefinitionAppService
{
    private readonly IReportRepository _reportRepository;
    private readonly SqlValidator _sqlValidator;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IConfiguration _configuration;

    public ReportDefinitionAppService(
        IReportRepository reportRepository,
        SqlValidator sqlValidator,
        IGuidGenerator guidGenerator,
        IConfiguration configuration)
    {
        _reportRepository = reportRepository;
        _sqlValidator = sqlValidator;
        _guidGenerator = guidGenerator;
        _configuration = configuration;
    }

    public async Task<PagedResultDto<ReportDefinitionSummaryDto>> GetListAsync(GetReportListInput input)
    {
        var count = await _reportRepository.GetCountAsync(input.Filter, input.IsActive);
        var list = await _reportRepository.GetListAsync(input.Filter, input.IsActive, input.SkipCount, input.MaxResultCount);
        return new PagedResultDto<ReportDefinitionSummaryDto>(
            count,
            ObjectMapper.Map<List<ReportDefinition>, List<ReportDefinitionSummaryDto>>(list));
    }

    public async Task<ReportDefinitionDto> GetAsync(Guid id)
    {
        var report = await _reportRepository.GetWithDetailsAsync(id)
            ?? throw new UserFriendlyException("Report not found.");
        return ObjectMapper.Map<ReportDefinition, ReportDefinitionDto>(report);
    }

    [Authorize(ReportBuilderPermissions.Reports.Admin.Create)]
    public async Task<ReportDefinitionDto> CreateAsync(CreateUpdateReportDefinitionDto input)
    {
        _sqlValidator.Validate(input.SqlQuery);
        var report = new ReportDefinition(
            _guidGenerator.Create(),
            input.Name,
            input.Description,
            input.SqlQuery,
            input.DisplayMode);
        ApplyColumns(report, input.Columns);
        ApplyParameters(report, input.Parameters);
        ApplyPermissions(report, input.Permissions);
        await _reportRepository.InsertAsync(report);
        return ObjectMapper.Map<ReportDefinition, ReportDefinitionDto>(report);
    }

    [Authorize(ReportBuilderPermissions.Reports.Admin.Edit)]
    public async Task<ReportDefinitionDto> UpdateAsync(Guid id, CreateUpdateReportDefinitionDto input)
    {
        _sqlValidator.Validate(input.SqlQuery);
        var report = await _reportRepository.GetWithDetailsAsync(id)
            ?? throw new UserFriendlyException("Report not found.");
        report.Update(input.Name, input.Description, input.SqlQuery, input.DisplayMode, input.IsActive);
        ApplyColumns(report, input.Columns);
        ApplyParameters(report, input.Parameters);
        ApplyPermissions(report, input.Permissions);
        await _reportRepository.UpdateAsync(report);
        return ObjectMapper.Map<ReportDefinition, ReportDefinitionDto>(report);
    }

    [Authorize(ReportBuilderPermissions.Reports.Admin.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _reportRepository.DeleteAsync(id);
    }

    public async Task<ReportDefinitionDto> ActivateAsync(Guid id)
    {
        var report = await _reportRepository.GetWithDetailsAsync(id)
            ?? throw new UserFriendlyException("Report not found.");
        report.Activate();
        await _reportRepository.UpdateAsync(report);
        return ObjectMapper.Map<ReportDefinition, ReportDefinitionDto>(report);
    }

    public async Task<ReportDefinitionDto> DeactivateAsync(Guid id)
    {
        var report = await _reportRepository.GetWithDetailsAsync(id)
            ?? throw new UserFriendlyException("Report not found.");
        report.Deactivate();
        await _reportRepository.UpdateAsync(report);
        return ObjectMapper.Map<ReportDefinition, ReportDefinitionDto>(report);
    }

    public async Task<List<ReportColumnDto>> DiscoverColumnsAsync(string sqlQuery)
    {
        _sqlValidator.Validate(sqlQuery);
        var connectionString = _configuration.GetConnectionString("ReadOnly")
            ?? _configuration.GetConnectionString("Default")
            ?? throw new UserFriendlyException("No database connection string configured.");

        await using var connection = new NpgsqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            var wrappedSql = $"SELECT * FROM ({sqlQuery}) AS __q LIMIT 0";
            await using var command = new NpgsqlCommand(wrappedSql, connection);
            command.CommandTimeout = ReportConsts.QueryTimeoutSeconds;

            await using var reader = await command.ExecuteReaderAsync(CommandBehavior.SchemaOnly);
            var columns = new List<ReportColumnDto>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var fieldName = reader.GetName(i);
                var fieldType = reader.GetFieldType(i);
                var isInternal = InternalColumns.Contains(fieldName);
                columns.Add(new ReportColumnDto
                {
                    FieldName = fieldName,
                    DisplayName = ToDisplayName(fieldName),
                    DataType = MapToColumnDataType(fieldType),
                    IsVisible = !isInternal,
                    IsFilterable = !isInternal,
                    IsSortable = !isInternal,
                    IsGroupable = false,
                    DisplayOrder = i,
                    ColumnPermissions = []
                });
            }
            return columns;
        }
        catch (NpgsqlException ex) when (ex.InnerException is TimeoutException || ex.Message.Contains("timeout", StringComparison.OrdinalIgnoreCase))
        {
            throw new UserFriendlyException("Column discovery query timed out. Please refine your query or contact an administrator.");
        }
        catch (NpgsqlException ex)
        {
            throw new UserFriendlyException($"Database error while discovering columns: {ex.Message}");
        }
    }

    // --- private helpers ---

    private void ApplyColumns(ReportDefinition report, List<CreateUpdateColumnDto> dtos)
    {
        var columns = dtos.Select((dto, _) =>
        {
            var col = new ReportColumn(
                _guidGenerator.Create(),
                report.Id,
                dto.FieldName,
                dto.DisplayName,
                dto.DataType,
                dto.IsVisible,
                dto.IsFilterable,
                dto.IsSortable,
                dto.IsGroupable,
                dto.DisplayOrder,
                dto.Width);
            foreach (var perm in dto.ColumnPermissions)
                col.AddPermission(_guidGenerator.Create(), perm.RoleName, perm.IsVisible, perm.IsFilterable);
            return col;
        }).ToList();
        report.SetColumns(columns);
    }

    private void ApplyParameters(ReportDefinition report, List<CreateUpdateParameterDto> dtos)
    {
        foreach (var existing in report.Parameters.ToList())
            report.RemoveParameter(existing.Id);
        foreach (var dto in dtos)
            report.AddParameter(
                _guidGenerator.Create(),
                dto.ParameterName,
                dto.DisplayName,
                dto.DataType,
                dto.DefaultValue,
                dto.IsRequired);
    }

    private void ApplyPermissions(ReportDefinition report, List<CreateUpdateReportPermissionDto> dtos)
    {
        foreach (var existing in report.Permissions.ToList())
            report.RemovePermission(existing.RoleName);
        foreach (var dto in dtos)
            report.SetPermission(_guidGenerator.Create(), dto.RoleName, dto.CanExport);
    }

    // Internal ABP framework columns — hidden by default in discovered reports
    private static readonly HashSet<string> InternalColumns = new(StringComparer.OrdinalIgnoreCase)
    {
        "ExtraProperties", "ConcurrencyStamp",
        "CreatorId", "CreationTime",
        "LastModifierId", "LastModificationTime",
        "IsDeleted", "DeleterId", "DeletionTime",
    };

    private static string ToDisplayName(string fieldName)
    {
        // Split PascalCase: "DateOfBirth" → "Date Of Birth", "GPAScore" → "GPA Score"
        var spaced = Regex.Replace(fieldName, @"([a-z])([A-Z])", "$1 $2");
        spaced = Regex.Replace(spaced, @"([A-Z]+)([A-Z][a-z])", "$1 $2");
        return CultureInfo.InvariantCulture.TextInfo
            .ToTitleCase(spaced.Replace("_", " ").ToLower());
    }

    private static ColumnDataType MapToColumnDataType(Type type)
    {
        if (type == typeof(int) || type == typeof(long) || type == typeof(decimal) ||
            type == typeof(float) || type == typeof(double) || type == typeof(short))
            return ColumnDataType.Number;
        if (type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(DateOnly))
            return ColumnDataType.Date;
        if (type == typeof(bool))
            return ColumnDataType.Bool;
        return ColumnDataType.String;
    }
}
