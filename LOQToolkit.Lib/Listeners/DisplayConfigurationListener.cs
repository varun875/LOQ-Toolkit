using System;
using System.Linq;
using System.Threading.Tasks;
using LOQToolkit.Lib.Extensions;
using LOQToolkit.Lib.System;
using LOQToolkit.Lib.Utils;
using Microsoft.Win32;

namespace LOQToolkit.Lib.Listeners;

public class DisplayConfigurationListener : IListener<DisplayConfigurationListener.ChangedEventArgs>
{
    public class ChangedEventArgs : EventArgs;

    private bool _started;

    public event EventHandler<ChangedEventArgs>? Changed;

    public Task StartAsync()
    {
        if (_started)
            return Task.CompletedTask;

        SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
        _started = true;

        return Task.CompletedTask;
    }

    public Task StopAsync()
    {
        SystemEvents.DisplaySettingsChanged -= SystemEvents_DisplaySettingsChanged;
        _started = false;

        return Task.CompletedTask;
    }

    private void SystemEvents_DisplaySettingsChanged(object? sender, EventArgs e)
    {
        if (Log.Instance.IsTraceEnabled)
            Log.Instance.Trace($"Event received.");

        InternalDisplay.SetNeedsRefresh();

        Changed?.Invoke(this, new());
    }
}
