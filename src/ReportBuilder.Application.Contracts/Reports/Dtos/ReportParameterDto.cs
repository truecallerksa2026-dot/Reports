using System;

namespace ReportBuilder.Reports;

public class ReportParameterDto
{
    public Guid Id { get; set; }
    public string ParameterName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public ParameterDataType DataType { get; set; }
    public string? DefaultValue { get; set; }
    public bool IsRequired { get; set; }
}
