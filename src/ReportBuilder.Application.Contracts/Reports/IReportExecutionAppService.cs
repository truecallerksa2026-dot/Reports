using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ReportBuilder.Reports;

public interface IReportExecutionAppService : IApplicationService
{
    Task<ReportResultDto> ExecuteAsync(Guid reportId, ExecuteReportInput input);
}
