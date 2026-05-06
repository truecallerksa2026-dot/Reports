using System.Collections.Generic;

namespace ReportBuilder.Reports;

public class CreateUpdateColumnDto
{
    public string FieldName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public ColumnDataType DataType { get; set; }
    public bool IsVisible { get; set; }
    public bool IsFilterable { get; set; }
    public bool IsSortable { get; set; }
    public bool IsGroupable { get; set; }
    public int DisplayOrder { get; set; }
    public int? Width { get; set; }
    public List<CreateUpdateColumnPermissionDto> ColumnPermissions { get; set; } = new();
}
