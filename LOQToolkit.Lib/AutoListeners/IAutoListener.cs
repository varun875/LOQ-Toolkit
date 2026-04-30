using System;
using System.Threading.Tasks;

namespace LOQToolkit.Lib.AutoListeners;

public interface IAutoListener<T> where T : EventArgs
{
    Task SubscribeChangedAsync(EventHandler<T> eventHandler);
    Task UnsubscribeChangedAsync(EventHandler<T> eventHandler);
}
