using Newtonsoft.Json;

namespace LOQToolkit.Lib.Automation.Steps;

[method: JsonConstructor]
public class PowerModeAutomationStep(PowerModeState state)
    : AbstractFeatureAutomationStep<PowerModeState>(state)
{
    public override IAutomationStep DeepCopy() => new PowerModeAutomationStep(State);
}
