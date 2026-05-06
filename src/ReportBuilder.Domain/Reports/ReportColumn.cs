using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ReportBuilder.Reports;

public class ReportColumn : Entity<Guid>
{
    public Guid ReportDefinitionId { get; private set; }
    public string FieldName { get; private set; } = default!;
    public string DisplayName { get; private set; } = default!;
    public ColumnDataType DataType { get; private set; }
    public bool IsVisible { get; private set; }
    public bool IsFilterable { get; private set; }
    public bool IsSortable { get; private set; }
    public bool IsGroupable { get; private set; }
    public int DisplayOrder { get; private set; }
    public int? Width { get; private set; }

    private readonly List<ColumnPermission> _columnPermissions = new();
    public IReadOnlyList<ColumnPermission> ColumnPermissions => _columnPermissions.AsReadOnly();

    protected ReportColumn() { }

    internal ReportColumn(
        Guid id,
        Guid reportDefinitionId,
        string fieldName,
        string displayName,
        ColumnDataType dataType,
        bool isVisible = true,
        bool isFilterable = true,
        bool isSortable = true,
        bool isGroupable = false,
        int displayOrder = 0,
        int? width = null)
    {
        Id = id;
        ReportDefinitionId = reportDefinitionId;
        FieldName = fieldName;
        DisplayName = displayName;
        DataType = dataType;
        IsVisible = isVisible;
        IsFilterable = isFilterable;
        IsSortable = isSortable;
        IsGroupable = isGroupable;
        DisplayOrder = displayOrder;
        Width = width;
    }

    public void Update(
        string displayName,
        ColumnDataType dataType,
        bool isVisible,
        bool isFilterable,
        bool isSortable,
        bool isGroupable,
        int displayOrder,
        int? width)
    {
        DisplayName = displayName;
        DataType = dataType;
        IsVisible = isVisible;
        IsFilterable = isFilterable;
        IsSortable = isSortable;
        IsGroupable = isGroupable;
        DisplayOrder = displayOrder;
        Width = width;
    }

    public void AddPermission(Guid id, string roleName, bool? isVisible, bool? isFilterable)
    {
        RemovePermissionForRole(roleName);
        _columnPermissions.Add(new ColumnPermission(id, Id, roleName, isVisible, isFilterable));
    }

    public void RemovePermissionForRole(string roleName)
    {
        _columnPermissions.RemoveAll(p => p.RoleName == roleName);
    }
}
