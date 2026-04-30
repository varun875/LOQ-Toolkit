using LOQToolkit.Lib;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.KeyboardBacklight.RGB;

public class RGBKeyboardBacklightEffectCardControl : AbstractComboBoxRGBKeyboardCardControl<RGBKeyboardBacklightEffect>
{
    public RGBKeyboardBacklightEffectCardControl()
    {
        Icon = SymbolRegular.Keyboard24;
        Title = Resource.RGBKeyboardBacklightEffectCardControl_Title;
    }
}
