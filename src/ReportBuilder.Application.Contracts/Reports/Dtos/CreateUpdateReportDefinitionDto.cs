using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportBuilder.Reports;

public class CreateUpdateReportDefinitionDto
{
    [Required]
    [MaxLength(ReportConsts.MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    [MaxLength(ReportConsts.MaxSqlQueryLength)]
    public string SqlQuery { get; set; } = string.Empty;

    public DisplayMode DisplayMode { get; set; }

    public bool IsActive { get; set; }

    public List<CreateUpdateColumnDto> Columns { get; set; } = new();

    public List<CreateUpdateParameterDto> Parameters { get; set; } = new();

    public List<CreateUpdateReportPermissionDto> Permissions { get; set; } = new();
}
