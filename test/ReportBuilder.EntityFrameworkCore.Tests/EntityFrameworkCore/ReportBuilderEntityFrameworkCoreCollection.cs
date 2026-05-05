using Xunit;

namespace ReportBuilder.EntityFrameworkCore;

[CollectionDefinition(ReportBuilderTestConsts.CollectionDefinitionName)]
public class ReportBuilderEntityFrameworkCoreCollection : ICollectionFixture<ReportBuilderEntityFrameworkCoreFixture>
{

}
