using Volo.Abp.Modularity;

namespace ReportBuilder;

[DependsOn(
    typeof(ReportBuilderApplicationModule),
    typeof(ReportBuilderDomainTestModule)
)]
public class ReportBuilderApplicationTestModule : AbpModule
{

}
