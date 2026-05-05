using System.Threading.Tasks;

namespace ReportBuilder.Data;

public interface IReportBuilderDbSchemaMigrator
{
    Task MigrateAsync();
}
