namespace ReportBuilder.Reports;

public class CreateUpdateReportPermissionDto
{
    public string RoleName { get; set; } = string.Empty;
    public bool CanExport { get; set; }
}
