using System;
using Volo.Abp.Application.Dtos;

namespace ReportBuilder.Reports;

public class ReportDefinitionSummaryDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DisplayMode DisplayMode { get; set; }
    public bool IsActive { get; set; }
}
