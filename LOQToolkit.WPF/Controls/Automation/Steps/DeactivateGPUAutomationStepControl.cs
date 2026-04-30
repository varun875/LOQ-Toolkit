using LOQToolkit.Lib.Automation;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class DeactivateGPUAutomationStepControl : AbstractComboBoxAutomationStepCardControl<DeactivateGPUAutomationStepState>
{
    public DeactivateGPUAutomationStepControl(DeactivateGPUAutomationStep step) : base(step)
    {
        Icon = SymbolRegular.DeveloperBoard24;
        Title = Resource.DeactivateGPUAutomationStepControl_Title;
        Subtitle = Resource.DeactivateGPUAutomationStepControl_Message;
    }
}
