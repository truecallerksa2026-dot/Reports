using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace ReportBuilder.Reports;

public class ReportDefinitionDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string SqlQuery { get; set; } = string.Empty;
    public DisplayMode DisplayMode { get; set; }
    public bool IsActive { get; set; }
    public List<ReportColumnDto> Columns { get; set; } = new();
    public List<ReportParameterDto> Parameters { get; set; } = new();
    public List<ReportPermissionDto> Permissions { get; set; } = new();
}
