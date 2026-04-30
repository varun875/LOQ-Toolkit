using System;
using System.Threading.Tasks;

namespace LOQToolkit.Lib.Listeners;

public interface IListener<TEventArgs> where TEventArgs : EventArgs
{
    event EventHandler<TEventArgs>? Changed;

    Task StartAsync();

    Task StopAsync();
}
