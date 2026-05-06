using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportBuilder.Controllers;

namespace ReportBuilder.Reports;

[Route("api/report-builder/reports")]
public class ReportExecutionController : ReportBuilderController
{
    private readonly IReportExecutionAppService _executionService;

    public ReportExecutionController(IReportExecutionAppService executionService)
    {
        _executionService = executionService;
    }

    [HttpPost("{id:guid}/execute")]
    public Task<ReportResultDto> ExecuteAsync(Guid id, [FromBody] ExecuteReportInput input) =>
        _executionService.ExecuteAsync(id, input);
}
