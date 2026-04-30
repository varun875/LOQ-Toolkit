using Newtonsoft.Json;

namespace LOQToolkit.Lib.Automation.Steps;

[method: JsonConstructor]
public class AlwaysOnUsbAutomationStep(AlwaysOnUSBState state)
    : AbstractFeatureAutomationStep<AlwaysOnUSBState>(state)
{
    public override IAutomationStep DeepCopy() => new AlwaysOnUsbAutomationStep(State);
}
