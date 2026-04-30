using System.Threading.Tasks;

namespace LOQToolkit.Lib.Features;

public interface IFeature<T> where T : struct
{
    Task<bool> IsSupportedAsync();
    Task<T[]> GetAllStatesAsync();
    Task<T> GetStateAsync();
    Task SetStateAsync(T state);
}
