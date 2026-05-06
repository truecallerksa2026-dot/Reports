namespace ReportBuilder.Reports;

public class CreateUpdateColumnPermissionDto
{
    public string RoleName { get; set; } = string.Empty;
    public bool? IsVisible { get; set; }
    public bool? IsFilterable { get; set; }
}
