using ReportBuilder.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ReportBuilder.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ReportBuilderController : AbpControllerBase
{
    protected ReportBuilderController()
    {
        LocalizationResource = typeof(ReportBuilderResource);
    }
}
