using Newtonsoft.Json;

namespace LOQToolkit.Lib.Automation.Steps;

[method: JsonConstructor]
public class SpeakerAutomationStep(SpeakerState state)
    : AbstractFeatureAutomationStep<SpeakerState>(state)
{
    public override IAutomationStep DeepCopy() => new SpeakerAutomationStep(State);
}
