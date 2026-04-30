using LOQToolkit.Lib;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class SpeakerAutomationStepControl : AbstractComboBoxAutomationStepCardControl<SpeakerState>
{
    public SpeakerAutomationStepControl(IAutomationStep<SpeakerState> step) : base(step)
    {
        Icon = SymbolRegular.Speaker224;
        Title = Resource.SpeakerAutomationStepControl_Title;
        Subtitle = Resource.SpeakerAutomationStepControl_Message;
    }
}
