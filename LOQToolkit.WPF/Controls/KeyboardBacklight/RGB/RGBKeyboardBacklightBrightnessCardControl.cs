using LOQToolkit.Lib;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.KeyboardBacklight.RGB;

public class RGBKeyboardBacklightBrightnessCardControl : AbstractComboBoxRGBKeyboardCardControl<RGBKeyboardBacklightBrightness>
{
    public RGBKeyboardBacklightBrightnessCardControl()
    {
        Icon = SymbolRegular.Keyboard24;
        Title = Resource.RGBKeyboardBacklightBrightnessCardControl_Brightness;
    }
}
