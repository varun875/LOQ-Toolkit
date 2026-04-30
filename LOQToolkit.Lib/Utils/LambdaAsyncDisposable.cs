using System;
using System.Threading.Tasks;

namespace LOQToolkit.Lib.Utils;

public class LambdaAsyncDisposable(Func<Task> action) : IAsyncDisposable
{
    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await action().ConfigureAwait(false);
    }
}
