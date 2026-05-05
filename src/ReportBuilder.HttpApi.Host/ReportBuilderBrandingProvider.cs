using Microsoft.Extensions.Localization;
using ReportBuilder.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ReportBuilder;

[Dependency(ReplaceServices = true)]
public class ReportBuilderBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ReportBuilderResource> _localizer;

    public ReportBuilderBrandingProvider(IStringLocalizer<ReportBuilderResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
