using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportBuilder.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ReportBuilder.Reports;

public class EfCoreReportRepository : EfCoreRepository<ReportBuilderDbContext, ReportDefinition, Guid>, IReportRepository
{
    public EfCoreReportRepository(IDbContextProvider<ReportBuilderDbContext> dbContextProvider)
        : base(dbContextProvider) { }

    public async Task<List<ReportDefinition>> GetListAsync(
        string? filter = null,
        bool? isActive = null,
        int skipCount = 0,
        int maxResultCount = 10,
        CancellationToken cancellationToken = default)
    {
        var query = await BuildQueryAsync(filter, isActive);
        return await query
            .OrderByDescending(r => r.CreationTime)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountAsync(
        string? filter = null,
        bool? isActive = null,
        CancellationToken cancellationToken = default)
    {
        var query = await BuildQueryAsync(filter, isActive);
        return await query.CountAsync(cancellationToken);
    }

    public async Task<ReportDefinition?> GetWithDetailsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.ReportDefinitions
            .Include(r => r.Columns)
                .ThenInclude(c => c.ColumnPermissions)
            .Include(r => r.Parameters)
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    private async Task<IQueryable<ReportDefinition>> BuildQueryAsync(string? filter, bool? isActive)
    {
        var dbContext = await GetDbContextAsync();
        return dbContext.ReportDefinitions
            .WhereIf(!filter.IsNullOrWhiteSpace(), r => r.Name.Contains(filter!))
            .WhereIf(isActive.HasValue, r => r.IsActive == isActive!.Value);
    }
}
