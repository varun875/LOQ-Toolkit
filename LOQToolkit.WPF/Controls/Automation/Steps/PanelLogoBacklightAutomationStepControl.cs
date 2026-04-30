using LOQToolkit.Lib;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class PanelLogoBacklightAutomationStepControl : AbstractComboBoxAutomationStepCardControl<PanelLogoBacklightState>
{
    public PanelLogoBacklightAutomationStepControl(IAutomationStep<PanelLogoBacklightState> step) : base(step)
    {
        Icon = SymbolRegular.LightbulbCircle24;
        Title = Resource.PanelLogoBacklightAutomationStepControl_Title;
        Subtitle = Resource.PanelLogoBacklightAutomationStepControl_Message;
    }
}
