using Newtonsoft.Json;

namespace LOQToolkit.Lib.Automation.Steps;

[method: JsonConstructor]
public class PortsBacklightAutomationStep(PortsBacklightState state)
    : AbstractFeatureAutomationStep<PortsBacklightState>(state)
{
    public override IAutomationStep DeepCopy() => new PortsBacklightAutomationStep(State);
}
