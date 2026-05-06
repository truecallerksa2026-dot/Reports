using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ReportBuilder.Reports;

public interface IReportDefinitionAppService : IApplicationService
{
    Task<PagedResultDto<ReportDefinitionSummaryDto>> GetListAsync(GetReportListInput input);
    Task<ReportDefinitionDto> GetAsync(Guid id);
    Task<ReportDefinitionDto> CreateAsync(CreateUpdateReportDefinitionDto input);
    Task<ReportDefinitionDto> UpdateAsync(Guid id, CreateUpdateReportDefinitionDto input);
    Task DeleteAsync(Guid id);
    Task<ReportDefinitionDto> ActivateAsync(Guid id);
    Task<ReportDefinitionDto> DeactivateAsync(Guid id);
    Task<List<ReportColumnDto>> DiscoverColumnsAsync(string sqlQuery); // dry-run SQL, return discovered columns
}
