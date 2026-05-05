using Volo.Abp.Settings;

namespace ReportBuilder.Settings;

public class ReportBuilderSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ReportBuilderSettings.MySetting1));
    }
}
