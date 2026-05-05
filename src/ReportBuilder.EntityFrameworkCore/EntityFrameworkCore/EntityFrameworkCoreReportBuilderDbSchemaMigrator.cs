using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReportBuilder.Data;
using Volo.Abp.DependencyInjection;

namespace ReportBuilder.EntityFrameworkCore;

public class EntityFrameworkCoreReportBuilderDbSchemaMigrator
    : IReportBuilderDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreReportBuilderDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the ReportBuilderDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ReportBuilderDbContext>()
            .Database
            .MigrateAsync();
    }
}
