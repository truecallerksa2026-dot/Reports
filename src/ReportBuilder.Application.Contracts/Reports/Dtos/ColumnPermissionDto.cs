using System;

namespace ReportBuilder.Reports;

public class ColumnPermissionDto
{
    public Guid Id { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public bool? IsVisible { get; set; }
    public bool? IsFilterable { get; set; }
}
