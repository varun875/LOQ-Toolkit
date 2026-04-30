using LOQToolkit.Lib.System.Management;

namespace LOQToolkit.Lib.Features.Hybrid;

public class IGPUModeGamezoneFeature()
    : AbstractWmiFeature<IGPUModeState>(WMI.LenovoGameZoneData.GetIGPUModeStatusAsync, WMI.LenovoGameZoneData.SetIGPUModeStatusAsync, WMI.LenovoGameZoneData.IsSupportIGPUModeAsync);
