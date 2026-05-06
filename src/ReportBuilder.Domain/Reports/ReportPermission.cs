using System;
using Volo.Abp.Domain.Entities;

namespace ReportBuilder.Reports;

public class ReportPermission : Entity<Guid>
{
    public Guid ReportDefinitionId { get; private set; }
    public string RoleName { get; private set; } = default!;
    public bool CanExport { get; private set; }

    protected ReportPermission() { }

    internal ReportPermission(Guid id, Guid reportDefinitionId, string roleName, bool canExport)
    {
        Id = id;
        ReportDefinitionId = reportDefinitionId;
        RoleName = roleName;
        CanExport = canExport;
    }
}
