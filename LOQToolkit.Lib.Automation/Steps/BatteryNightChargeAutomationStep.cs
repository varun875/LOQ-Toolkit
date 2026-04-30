using Newtonsoft.Json;

namespace LOQToolkit.Lib.Automation.Steps;

[method: JsonConstructor]
public class BatteryNightChargeAutomationStep(BatteryNightChargeState state)
    : AbstractFeatureAutomationStep<BatteryNightChargeState>(state)
{
    public override IAutomationStep DeepCopy() => new BatteryNightChargeAutomationStep(State);
}
