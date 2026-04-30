using System;
using System.Threading.Tasks;

namespace LOQToolkit.Lib.Utils;

public interface IMainThreadDispatcher
{
    void Dispatch(Action callback);

    Task DispatchAsync(Func<Task> callback);
}
