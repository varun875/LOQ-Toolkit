using System.Collections.Generic;

namespace LOQToolkit.Lib.SoftwareDisabler;

public class LegionZoneDisabler : AbstractSoftwareDisabler
{
    protected override IEnumerable<string> ScheduledTasksPaths => [];
    protected override IEnumerable<string> ServiceNames => ["LZService"];
    protected override IEnumerable<string> ProcessNames => ["LegionZone", "LZTray"];
}
