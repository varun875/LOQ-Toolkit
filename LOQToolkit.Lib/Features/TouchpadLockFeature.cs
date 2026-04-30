using LOQToolkit.Lib.System.Management;

namespace LOQToolkit.Lib.Features;

public class TouchpadLockFeature()
    : AbstractWmiFeature<TouchpadLockState>(WMI.LenovoGameZoneData.GetTPStatusStatusAsync, WMI.LenovoGameZoneData.SetTPStatusAsync, WMI.LenovoGameZoneData.IsSupportDisableTPAsync);
