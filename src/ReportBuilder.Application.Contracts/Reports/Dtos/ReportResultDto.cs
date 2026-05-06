using System.Collections.Generic;

namespace ReportBuilder.Reports;

public class ReportResultDto
{
    public List<ReportColumnDto> Columns { get; set; } = new();
    public List<Dictionary<string, object?>> Rows { get; set; } = new();
    public int TotalCount { get; set; }
}
