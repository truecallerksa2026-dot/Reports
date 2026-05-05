using Volo.Abp.Modularity;

namespace ReportBuilder;

[DependsOn(
    typeof(ReportBuilderDomainModule),
    typeof(ReportBuilderTestBaseModule)
)]
public class ReportBuilderDomainTestModule : AbpModule
{

}
