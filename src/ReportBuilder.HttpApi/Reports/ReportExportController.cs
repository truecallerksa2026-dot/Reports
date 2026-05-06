using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportBuilder.Controllers;

namespace ReportBuilder.Reports;

[Route("api/report-builder/reports")]
public class ReportExportController : ReportBuilderController
{
    private readonly IReportExecutionAppService _executionService;

    public ReportExportController(IReportExecutionAppService executionService)
    {
        _executionService = executionService;
    }

    /// <summary>
    /// Export report as PDF or Excel.
    /// TODO: Implement with DevExpress XtraReports once the NuGet feed is configured.
    /// When ready: add DevExpress.AspNetCore.Reporting NuGet package and replace the stub below.
    /// </summary>
    [HttpPost("{id:guid}/export")]
    public async Task<IActionResult> ExportAsync(Guid id, [FromQuery] string format = "pdf")
    {
        // Get the data via the execution service
        var result = await _executionService.ExecuteAsync(id, new ExecuteReportInput
        {
            MaxResultCount = ReportConsts.MaxRowLimit
        });

        // TODO: Replace with XtraReports PDF/Excel generation
        // When DevExpress license is available:
        // 1. Add DevExpress.AspNetCore.Reporting NuGet package
        // 2. Build XtraReport programmatically from result.Columns + result.Rows
        // 3. Export to MemoryStream using report.ExportToPdf() or report.ExportToXlsx()
        // 4. Return File(stream, contentType, fileName)

        return StatusCode(501, new
        {
            message = "Export functionality requires DevExpress license configuration. See TODO in ReportExportController.",
            reportId = id,
            format = format,
            rowCount = result.TotalCount
        });
    }
}
