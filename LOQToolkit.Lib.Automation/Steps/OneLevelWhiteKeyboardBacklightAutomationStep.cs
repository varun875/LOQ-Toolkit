using Newtonsoft.Json;

namespace LOQToolkit.Lib.Automation.Steps;

[method: JsonConstructor]
public class OneLevelWhiteKeyboardBacklightAutomationStep(OneLevelWhiteKeyboardBacklightState state)
    : AbstractFeatureAutomationStep<OneLevelWhiteKeyboardBacklightState>(state)
{
    public override IAutomationStep DeepCopy() => new OneLevelWhiteKeyboardBacklightAutomationStep(State);
}
