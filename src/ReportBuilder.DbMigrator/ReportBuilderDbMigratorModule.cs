using ReportBuilder.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace ReportBuilder.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ReportBuilderEntityFrameworkCoreModule),
    typeof(ReportBuilderApplicationContractsModule),
    typeof(ReportBuilderApplicationModule)
)]
public class ReportBuilderDbMigratorModule : AbpModule
{
}
