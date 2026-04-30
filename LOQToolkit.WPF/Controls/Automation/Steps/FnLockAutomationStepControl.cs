using LOQToolkit.Lib;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class FnLockAutomationStepControl : AbstractComboBoxAutomationStepCardControl<FnLockState>
{
    public FnLockAutomationStepControl(IAutomationStep<FnLockState> step) : base(step)
    {
        Icon = SymbolRegular.Keyboard24;
        Title = Resource.FnLockAutomationStepControl_Title;
        Subtitle = Resource.FnLockAutomationStepControl_Message;
    }
}
