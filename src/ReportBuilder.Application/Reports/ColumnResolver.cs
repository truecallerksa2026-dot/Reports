using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace ReportBuilder.Reports;

public class ColumnResolver : ITransientDependency
{
    /// <summary>
    /// Returns the list of columns visible to the user, with per-role overrides applied.
    /// </summary>
    public List<ReportColumn> ResolveVisibleColumns(ReportDefinition report, IEnumerable<string> userRoles)
    {
        var roles = userRoles.ToHashSet(StringComparer.OrdinalIgnoreCase);

        return report.Columns
            .OrderBy(c => c.DisplayOrder)
            .Where(c => IsColumnVisible(c, roles))
            .ToList();
    }

    /// <summary>
    /// Resolves effective IsVisible for a column given user roles.
    /// </summary>
    private static bool IsColumnVisible(ReportColumn column, HashSet<string> userRoles)
    {
        var override_ = column.ColumnPermissions
            .FirstOrDefault(p => userRoles.Contains(p.RoleName));

        return override_?.IsVisible ?? column.IsVisible;
    }

    /// <summary>
    /// Resolves effective IsFilterable for a column given user roles.
    /// </summary>
    public bool IsColumnFilterable(ReportColumn column, IEnumerable<string> userRoles)
    {
        var roles = userRoles.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var override_ = column.ColumnPermissions
            .FirstOrDefault(p => roles.Contains(p.RoleName));

        return override_?.IsFilterable ?? column.IsFilterable;
    }
}
