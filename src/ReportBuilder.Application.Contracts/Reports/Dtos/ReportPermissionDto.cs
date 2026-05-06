using System;

namespace ReportBuilder.Reports;

public class ReportPermissionDto
{
    public Guid Id { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public bool CanExport { get; set; }
}
