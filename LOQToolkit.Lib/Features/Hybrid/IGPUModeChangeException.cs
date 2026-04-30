using System;

namespace LOQToolkit.Lib.Features.Hybrid;

public class IGPUModeChangeException(IGPUModeState igpuMode) : Exception
{
    public IGPUModeState IGPUMode { get; } = igpuMode;
}
