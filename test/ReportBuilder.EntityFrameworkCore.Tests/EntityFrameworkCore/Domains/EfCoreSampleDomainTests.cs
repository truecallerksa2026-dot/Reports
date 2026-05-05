using ReportBuilder.Samples;
using Xunit;

namespace ReportBuilder.EntityFrameworkCore.Domains;

[Collection(ReportBuilderTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ReportBuilderEntityFrameworkCoreTestModule>
{

}
