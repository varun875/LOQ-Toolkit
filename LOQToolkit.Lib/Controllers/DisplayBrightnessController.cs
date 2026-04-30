using System.Threading.Tasks;
using LOQToolkit.Lib.System.Management;

namespace LOQToolkit.Lib.Controllers;

public class DisplayBrightnessController
{
    public Task SetBrightnessAsync(int brightness) => WMI.WmiMonitorBrightnessMethods.WmiSetBrightness(brightness, 1);
}
