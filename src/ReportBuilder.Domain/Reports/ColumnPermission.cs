using System;
using Volo.Abp.Domain.Entities;

namespace ReportBuilder.Reports;

public class ColumnPermission : Entity<Guid>
{
    public Guid ReportColumnId { get; private set; }
    public string RoleName { get; private set; } = default!;
    public bool? IsVisible { get; private set; }
    public bool? IsFilterable { get; private set; }

    protected ColumnPermission() { }

    internal ColumnPermission(Guid id, Guid reportColumnId, string roleName, bool? isVisible, bool? isFilterable)
    {
        Id = id;
        ReportColumnId = reportColumnId;
        RoleName = roleName;
        IsVisible = isVisible;
        IsFilterable = isFilterable;
    }
}
