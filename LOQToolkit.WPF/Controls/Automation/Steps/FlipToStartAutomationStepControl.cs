using LOQToolkit.Lib;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class FlipToStartAutomationStepControl : AbstractComboBoxAutomationStepCardControl<FlipToStartState>
{
    public FlipToStartAutomationStepControl(IAutomationStep<FlipToStartState> step) : base(step)
    {
        Icon = SymbolRegular.Power24;
        Title = Resource.FlipToStartAutomationStepControl_Title;
        Subtitle = Resource.FlipToStartAutomationStepControl_Message;
    }
}
