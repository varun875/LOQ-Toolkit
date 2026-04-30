using LOQToolkit.Lib;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class BatteryAutomationStepControl : AbstractComboBoxAutomationStepCardControl<BatteryState>
{
    public BatteryAutomationStepControl(IAutomationStep<BatteryState> step) : base(step)
    {
        Icon = SymbolRegular.BatteryCharge24;
        Title = Resource.BatteryAutomationStepControl_Title;
        Subtitle = Resource.BatteryAutomationStepControl_Message;
    }
}
