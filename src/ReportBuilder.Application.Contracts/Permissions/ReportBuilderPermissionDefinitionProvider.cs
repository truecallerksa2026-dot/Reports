using ReportBuilder.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace ReportBuilder.Permissions;

public class ReportBuilderPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ReportBuilderPermissions.GroupName);

        myGroup.AddPermission(ReportBuilderPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(ReportBuilderPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        var students = myGroup.AddPermission(ReportBuilderPermissions.Students.Default, L("Permission:Students"));
        students.AddChild(ReportBuilderPermissions.Students.Create, L("Permission:Students.Create"));
        students.AddChild(ReportBuilderPermissions.Students.Edit, L("Permission:Students.Edit"));
        students.AddChild(ReportBuilderPermissions.Students.Delete, L("Permission:Students.Delete"));

        var reports = myGroup.AddPermission(ReportBuilderPermissions.Reports.Default, L("Permission:Reports"));

        var admin = reports.AddChild(ReportBuilderPermissions.Reports.Admin.Default, L("Permission:Reports.Admin"));
        admin.AddChild(ReportBuilderPermissions.Reports.Admin.Create, L("Permission:Reports.Admin.Create"));
        admin.AddChild(ReportBuilderPermissions.Reports.Admin.Edit, L("Permission:Reports.Admin.Edit"));
        admin.AddChild(ReportBuilderPermissions.Reports.Admin.Delete, L("Permission:Reports.Admin.Delete"));
        admin.AddChild(ReportBuilderPermissions.Reports.Admin.TestRun, L("Permission:Reports.Admin.TestRun"));

        var viewer = reports.AddChild(ReportBuilderPermissions.Reports.Viewer.Default, L("Permission:Reports.Viewer"));
        viewer.AddChild(ReportBuilderPermissions.Reports.Viewer.Grid, L("Permission:Reports.Viewer.Grid"));
        viewer.AddChild(ReportBuilderPermissions.Reports.Viewer.Report, L("Permission:Reports.Viewer.Report"));
        viewer.AddChild(ReportBuilderPermissions.Reports.Viewer.Export, L("Permission:Reports.Viewer.Export"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ReportBuilderResource>(name);
    }
}
