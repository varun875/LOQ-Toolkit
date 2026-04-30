using LOQToolkit.Lib;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class AlwaysOnUsbAutomationStepControl : AbstractComboBoxAutomationStepCardControl<AlwaysOnUSBState>
{
    public AlwaysOnUsbAutomationStepControl(IAutomationStep<AlwaysOnUSBState> step) : base(step)
    {
        Icon = SymbolRegular.UsbStick24;
        Title = Resource.AlwaysOnUsbAutomationStepControl_Title;
        Subtitle = Resource.AlwaysOnUsbAutomationStepControl_Message;
    }
}
