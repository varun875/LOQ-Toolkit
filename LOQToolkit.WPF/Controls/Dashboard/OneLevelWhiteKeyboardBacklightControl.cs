using LOQToolkit.Lib;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Dashboard;

internal class OneLevelWhiteKeyboardBacklightControl : AbstractToggleFeatureCardControl<OneLevelWhiteKeyboardBacklightState>
{
    protected override OneLevelWhiteKeyboardBacklightState OnState => OneLevelWhiteKeyboardBacklightState.On;

    protected override OneLevelWhiteKeyboardBacklightState OffState => OneLevelWhiteKeyboardBacklightState.Off;

    public OneLevelWhiteKeyboardBacklightControl()
    {
        Icon = SymbolRegular.Keyboard24;
        Title = Resource.OneLevelWhiteKeyboardBacklightControl_Title;
        Subtitle = Resource.OneLevelWhiteKeyboardBacklightControl_Message;
    }
}
