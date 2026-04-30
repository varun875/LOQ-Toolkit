using Newtonsoft.Json;

namespace LOQToolkit.Lib.Automation.Steps;

[method: JsonConstructor]
public class HybridModeAutomationStep(HybridModeState state)
    : AbstractFeatureAutomationStep<HybridModeState>(state)
{
    public override IAutomationStep DeepCopy() => new HybridModeAutomationStep(State);
}
