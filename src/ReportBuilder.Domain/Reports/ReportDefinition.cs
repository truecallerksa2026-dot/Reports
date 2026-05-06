using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;

namespace ReportBuilder.Reports;

public class ReportDefinition : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public string SqlQuery { get; private set; } = default!;
    public DisplayMode DisplayMode { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<ReportColumn> _columns = new();
    public IReadOnlyList<ReportColumn> Columns => _columns.AsReadOnly();

    private readonly List<ReportParameter> _parameters = new();
    public IReadOnlyList<ReportParameter> Parameters => _parameters.AsReadOnly();

    private readonly List<ReportPermission> _permissions = new();
    public IReadOnlyList<ReportPermission> Permissions => _permissions.AsReadOnly();

    protected ReportDefinition() { }

    public ReportDefinition(Guid id, string name, string? description, string sqlQuery, DisplayMode displayMode)
    {
        Id = id;
        Name = name;
        Description = description;
        SqlQuery = sqlQuery;
        DisplayMode = displayMode;
        IsActive = true;
    }

    public void Update(string name, string? description, string sqlQuery, DisplayMode displayMode, bool isActive)
    {
        Name = name;
        Description = description;
        SqlQuery = sqlQuery;
        DisplayMode = displayMode;
        IsActive = isActive;
    }

    public void SetColumns(IEnumerable<ReportColumn> columns)
    {
        _columns.Clear();
        _columns.AddRange(columns);
    }

    public void AddParameter(
        Guid id,
        string parameterName,
        string displayName,
        ParameterDataType dataType,
        string? defaultValue,
        bool isRequired)
    {
        _parameters.Add(new ReportParameter(id, Id, parameterName, displayName, dataType, defaultValue, isRequired));
    }

    public void RemoveParameter(Guid parameterId)
    {
        _parameters.RemoveAll(p => p.Id == parameterId);
    }

    public void SetPermission(Guid id, string roleName, bool canExport)
    {
        var existing = _permissions.FirstOrDefault(p => p.RoleName == roleName);
        if (existing != null)
        {
            _permissions.Remove(existing);
        }

        _permissions.Add(new ReportPermission(id, Id, roleName, canExport));
    }

    public void RemovePermission(string roleName)
    {
        _permissions.RemoveAll(p => p.RoleName == roleName);
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
