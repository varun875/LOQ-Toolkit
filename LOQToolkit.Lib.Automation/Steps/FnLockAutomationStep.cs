using Newtonsoft.Json;

namespace LOQToolkit.Lib.Automation.Steps;

[method: JsonConstructor]
public class FnLockAutomationStep(FnLockState state)
    : AbstractFeatureAutomationStep<FnLockState>(state)
{
    public override IAutomationStep DeepCopy() => new FnLockAutomationStep(State);
}
