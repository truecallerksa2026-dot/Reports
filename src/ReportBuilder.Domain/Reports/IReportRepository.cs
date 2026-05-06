using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ReportBuilder.Reports;

public interface IReportRepository : IRepository<ReportDefinition, Guid>
{
    Task<List<ReportDefinition>> GetListAsync(
        string? filter = null,
        bool? isActive = null,
        int skipCount = 0,
        int maxResultCount = 10,
        CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(
        string? filter = null,
        bool? isActive = null,
        CancellationToken cancellationToken = default);

    Task<ReportDefinition?> GetWithDetailsAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
