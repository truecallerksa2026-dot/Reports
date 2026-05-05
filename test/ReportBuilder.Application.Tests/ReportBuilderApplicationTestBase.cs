using Volo.Abp.Modularity;

namespace ReportBuilder;

public abstract class ReportBuilderApplicationTestBase<TStartupModule> : ReportBuilderTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
