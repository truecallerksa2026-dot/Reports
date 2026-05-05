using Volo.Abp.Modularity;

namespace ReportBuilder;

/* Inherit from this class for your domain layer tests. */
public abstract class ReportBuilderDomainTestBase<TStartupModule> : ReportBuilderTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
