using LOQToolkit.Lib;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class PowerModeAutomationStepControl : AbstractComboBoxAutomationStepCardControl<PowerModeState>
{
    public PowerModeAutomationStepControl(IAutomationStep<PowerModeState> step) : base(step)
    {
        Icon = SymbolRegular.Gauge24;
        Title = Resource.PowerModeAutomationStepControl_Title;
        Subtitle = Resource.PowerModeAutomationStepControl_Message;
    }
}
