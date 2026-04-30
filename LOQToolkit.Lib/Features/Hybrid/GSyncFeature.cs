using LOQToolkit.Lib.System.Management;

namespace LOQToolkit.Lib.Features.Hybrid;

public class GSyncFeature()
    : AbstractWmiFeature<GSyncState>(WMI.LenovoGameZoneData.GetGSyncStatusAsync, WMI.LenovoGameZoneData.SetGSyncStatusAsync, WMI.LenovoGameZoneData.IsSupportGSyncAsync);
