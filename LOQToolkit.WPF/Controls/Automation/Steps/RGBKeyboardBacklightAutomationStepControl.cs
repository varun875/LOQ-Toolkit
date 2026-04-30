using LOQToolkit.Lib;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class RGBKeyboardBacklightAutomationStepControl : AbstractComboBoxAutomationStepCardControl<RGBKeyboardBacklightPreset>
{
    public RGBKeyboardBacklightAutomationStepControl(IAutomationStep<RGBKeyboardBacklightPreset> step) : base(step)
    {
        Icon = SymbolRegular.Keyboard24;
        Title = Resource.RGBKeyboardBacklightAutomationStepControl_Title;
        Subtitle = Resource.RGBKeyboardBacklightAutomationStepControl_Message;
    }
}
