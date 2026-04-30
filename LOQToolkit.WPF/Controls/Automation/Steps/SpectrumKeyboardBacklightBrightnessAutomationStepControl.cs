using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class SpectrumKeyboardBacklightBrightnessAutomationStepControl : AbstractComboBoxAutomationStepCardControl<int>
{
    public SpectrumKeyboardBacklightBrightnessAutomationStepControl(IAutomationStep<int> step) : base(step)
    {
        Icon = SymbolRegular.BrightnessHigh24;
        Title = Resource.SpectrumKeyboardBacklightBrightnessAutomationStepControl_Title;
        Subtitle = Resource.SpectrumKeyboardBacklightBrightnessAutomationStepControl_Message;
    }

    protected override string ComboBoxItemDisplayName(int value)
    {
        return value == 0
            ? Resource.SpectrumKeyboardBacklightBrightnessAutomationStepControl_Off
            : base.ComboBoxItemDisplayName(value);
    }
}
