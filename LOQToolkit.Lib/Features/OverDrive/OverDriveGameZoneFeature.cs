using LOQToolkit.Lib.System.Management;

namespace LOQToolkit.Lib.Features.OverDrive;

public class OverDriveGameZoneFeature()
    : AbstractWmiFeature<OverDriveState>(WMI.LenovoGameZoneData.GetODStatusAsync, WMI.LenovoGameZoneData.SetODStatusAsync, WMI.LenovoGameZoneData.IsSupportODAsync);
