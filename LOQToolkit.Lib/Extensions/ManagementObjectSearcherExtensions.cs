using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace LOQToolkit.Lib.Extensions;

public static class ManagementObjectSearcherExtensions
{
    public static async Task<IEnumerable<ManagementBaseObject>> GetAsync(this ManagementObjectSearcher mos)
    {
        return await Task.Run(() =>
        {
            try
            {
                return mos.Get().Cast<ManagementBaseObject>();
            }
            finally
            {
                mos.Dispose();
            }
        }).ConfigureAwait(false);
    }
}
