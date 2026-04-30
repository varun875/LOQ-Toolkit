using LOQToolkit.Lib;
using LOQToolkit.Lib.Listeners;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Dashboard;

public class PortsBacklightControl : AbstractToggleFeatureCardControl<PortsBacklightState>
{
    private readonly LightingChangeListener _listener = IoCContainer.Resolve<LightingChangeListener>();

    protected override PortsBacklightState OnState => PortsBacklightState.On;

    protected override PortsBacklightState OffState => PortsBacklightState.Off;

    public PortsBacklightControl()
    {
        Icon = SymbolRegular.UsbPlug24;
        Title = Resource.PortsBacklightControl_Title;
        Subtitle = Resource.PortsBacklightControl_Message;

        _listener.Changed += Listener_Changed;
    }

    private void Listener_Changed(object? sender, LightingChangeListener.ChangedEventArgs e) => Dispatcher.Invoke(async () =>
    {
        if (e.State != LightingChangeState.Ports)
            return;

        await RefreshAsync();
    });
}
