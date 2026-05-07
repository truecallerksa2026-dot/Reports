using System.Threading.Tasks;
using ReportBuilder.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.PermissionManagement;

namespace ReportBuilder;

/// <summary>
/// Grants all ReportBuilder permissions to the built-in admin role on seed.
/// ABP's IdentityDataSeedContributor creates the admin role but only seeds
/// framework-level permissions — custom module permissions must be granted here.
/// </summary>
public class ReportBuilderPermissionDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private const string AdminRoleName = "admin";

    private readonly IPermissionManager _permissionManager;

    public ReportBuilderPermissionDataSeedContributor(IPermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await GrantAsync(ReportBuilderPermissions.Dashboard.Host);

        await GrantAsync(ReportBuilderPermissions.Students.Default);
        await GrantAsync(ReportBuilderPermissions.Students.Create);
        await GrantAsync(ReportBuilderPermissions.Students.Edit);
        await GrantAsync(ReportBuilderPermissions.Students.Delete);

        await GrantAsync(ReportBuilderPermissions.Reports.Default);

        await GrantAsync(ReportBuilderPermissions.Reports.Admin.Default);
        await GrantAsync(ReportBuilderPermissions.Reports.Admin.Create);
        await GrantAsync(ReportBuilderPermissions.Reports.Admin.Edit);
        await GrantAsync(ReportBuilderPermissions.Reports.Admin.Delete);
        await GrantAsync(ReportBuilderPermissions.Reports.Admin.TestRun);

        await GrantAsync(ReportBuilderPermissions.Reports.Viewer.Default);
        await GrantAsync(ReportBuilderPermissions.Reports.Viewer.Grid);
        await GrantAsync(ReportBuilderPermissions.Reports.Viewer.Report);
        await GrantAsync(ReportBuilderPermissions.Reports.Viewer.Export);
    }

    private async Task GrantAsync(string permission)
    {
        await _permissionManager.SetForRoleAsync(AdminRoleName, permission, isGranted: true);
    }
}
