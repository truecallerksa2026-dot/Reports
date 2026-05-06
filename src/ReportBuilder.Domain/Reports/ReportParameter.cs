using System;
using Volo.Abp.Domain.Entities;

namespace ReportBuilder.Reports;

public class ReportParameter : Entity<Guid>
{
    public Guid ReportDefinitionId { get; private set; }
    public string ParameterName { get; private set; } = default!;
    public string DisplayName { get; private set; } = default!;
    public ParameterDataType DataType { get; private set; }
    public string? DefaultValue { get; private set; }
    public bool IsRequired { get; private set; }

    protected ReportParameter() { }

    internal ReportParameter(
        Guid id,
        Guid reportDefinitionId,
        string parameterName,
        string displayName,
        ParameterDataType dataType,
        string? defaultValue = null,
        bool isRequired = false)
    {
        Id = id;
        ReportDefinitionId = reportDefinitionId;
        ParameterName = parameterName;
        DisplayName = displayName;
        DataType = dataType;
        DefaultValue = defaultValue;
        IsRequired = isRequired;
    }
}
