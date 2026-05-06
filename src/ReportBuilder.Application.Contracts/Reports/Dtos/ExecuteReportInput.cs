using System.Collections.Generic;

namespace ReportBuilder.Reports;

public class ExecuteReportInput
{
    public Dictionary<string, object?> Parameters { get; set; } = new();
    public int SkipCount { get; set; } = 0;
    public int MaxResultCount { get; set; } = 100;
    public string? SortField { get; set; }
    public bool SortDescending { get; set; } = false;
}
