using ReportBuilder.Samples;
using Xunit;

namespace ReportBuilder.EntityFrameworkCore.Applications;

[Collection(ReportBuilderTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ReportBuilderEntityFrameworkCoreTestModule>
{

}
