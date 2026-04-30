using LOQToolkit.Lib;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class TouchpadLockAutomationStepControl : AbstractComboBoxAutomationStepCardControl<TouchpadLockState>
{
    public TouchpadLockAutomationStepControl(IAutomationStep<TouchpadLockState> step) : base(step)
    {
        Icon = SymbolRegular.Tablet24;
        Title = Resource.TouchpadLockAutomationStepControl_Title;
        Subtitle = Resource.TouchpadLockAutomationStepControl_Message;
    }
}
