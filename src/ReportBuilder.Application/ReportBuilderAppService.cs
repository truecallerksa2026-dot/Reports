using ReportBuilder.Localization;
using Volo.Abp.Application.Services;

namespace ReportBuilder;

/* Inherit your application services from this class.
 */
public abstract class ReportBuilderAppService : ApplicationService
{
    protected ReportBuilderAppService()
    {
        LocalizationResource = typeof(ReportBuilderResource);
    }
}
