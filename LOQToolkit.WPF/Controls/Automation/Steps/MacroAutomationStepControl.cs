using LOQToolkit.Lib.Automation;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class MacroAutomationStepControl : AbstractComboBoxAutomationStepCardControl<MacroAutomationStepState>
{
    public MacroAutomationStepControl(IAutomationStep<MacroAutomationStepState> step) : base(step)
    {
        Icon = SymbolRegular.ReceiptPlay24;
        Title = Resource.MacroAutomationStepControl_Title;
        Subtitle = Resource.MacroAutomationStepControl_Message;
    }
}
