using LOQToolkit.Lib;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class BatteryNightChargeAutomationStepControl : AbstractComboBoxAutomationStepCardControl<BatteryNightChargeState>
{
    public BatteryNightChargeAutomationStepControl(IAutomationStep<BatteryNightChargeState> step) : base(step)
    {
        Icon = SymbolRegular.WeatherMoon24;
        Title = Resource.BatteryNightChargeAutomationStepControl_Title;
        Subtitle = Resource.BatteryNightChargeAutomationStepControl_Message;
    }
}
