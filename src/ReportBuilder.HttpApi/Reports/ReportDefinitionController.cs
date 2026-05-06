using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportBuilder.Controllers;
using Volo.Abp.Application.Dtos;

namespace ReportBuilder.Reports;

[Route("api/report-builder/reports")]
public class ReportDefinitionController : ReportBuilderController
{
    private readonly IReportDefinitionAppService _reportService;

    public ReportDefinitionController(IReportDefinitionAppService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet]
    public Task<PagedResultDto<ReportDefinitionSummaryDto>> GetListAsync([FromQuery] GetReportListInput input) =>
        _reportService.GetListAsync(input);

    [HttpGet("{id:guid}")]
    public Task<ReportDefinitionDto> GetAsync(Guid id) =>
        _reportService.GetAsync(id);

    [HttpPost]
    public Task<ReportDefinitionDto> CreateAsync([FromBody] CreateUpdateReportDefinitionDto input) =>
        _reportService.CreateAsync(input);

    [HttpPut("{id:guid}")]
    public Task<ReportDefinitionDto> UpdateAsync(Guid id, [FromBody] CreateUpdateReportDefinitionDto input) =>
        _reportService.UpdateAsync(id, input);

    [HttpDelete("{id:guid}")]
    public Task DeleteAsync(Guid id) =>
        _reportService.DeleteAsync(id);

    [HttpPost("{id:guid}/activate")]
    public Task<ReportDefinitionDto> ActivateAsync(Guid id) =>
        _reportService.ActivateAsync(id);

    [HttpPost("{id:guid}/deactivate")]
    public Task<ReportDefinitionDto> DeactivateAsync(Guid id) =>
        _reportService.DeactivateAsync(id);

    [HttpPost("discover-columns")]
    public Task<List<ReportColumnDto>> DiscoverColumnsAsync([FromBody] DiscoverColumnsInput input) =>
        _reportService.DiscoverColumnsAsync(input.SqlQuery);
}
