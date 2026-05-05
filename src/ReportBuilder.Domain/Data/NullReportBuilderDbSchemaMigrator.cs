using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ReportBuilder.Data;

/* This is used if database provider does't define
 * IReportBuilderDbSchemaMigrator implementation.
 */
public class NullReportBuilderDbSchemaMigrator : IReportBuilderDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
