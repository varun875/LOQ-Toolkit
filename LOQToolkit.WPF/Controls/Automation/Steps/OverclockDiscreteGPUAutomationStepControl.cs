using LOQToolkit.Lib.Automation;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class OverclockDiscreteGPUAutomationStepControl : AbstractComboBoxAutomationStepCardControl<OverclockDiscreteGPUAutomationStepState>
{
    public OverclockDiscreteGPUAutomationStepControl(IAutomationStep<OverclockDiscreteGPUAutomationStepState> step) : base(step)
    {
        Icon = SymbolRegular.DeveloperBoardLightning20;
        Title = Resource.OverclockDiscreteGPUAutomationStepControl_Title;
        Subtitle = Resource.OverclockDiscreteGPUAutomationStepControl_Message;
    }
}
