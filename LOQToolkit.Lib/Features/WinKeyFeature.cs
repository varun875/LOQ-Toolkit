using LOQToolkit.Lib.System.Management;

namespace LOQToolkit.Lib.Features;

public class WinKeyFeature()
    : AbstractWmiFeature<WinKeyState>(WMI.LenovoGameZoneData.GetWinKeyStatusAsync, WMI.LenovoGameZoneData.SetWinKeyStatusAsync, WMI.LenovoGameZoneData.IsSupportDisableWinKeyAsync);
