using LOQToolkit.Lib;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class WhiteKeyboardBacklightAutomationStepControl : AbstractComboBoxAutomationStepCardControl<WhiteKeyboardBacklightState>
{
    public WhiteKeyboardBacklightAutomationStepControl(IAutomationStep<WhiteKeyboardBacklightState> step) : base(step)
    {
        Icon = SymbolRegular.Keyboard24;
        Title = Resource.WhiteKeyboardBacklightAutomationStepControl_Title;
        Subtitle = Resource.WhiteKeyboardBacklightAutomationStepControl_Message;
    }
}
