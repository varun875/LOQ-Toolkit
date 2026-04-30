using LOQToolkit.Lib.Automation.Pipeline.Triggers;

namespace LOQToolkit.WPF.Windows.Automation.TabItemContent;

public interface IAutomationPipelineTriggerTabItemContent<out T> where T : IAutomationPipelineTrigger
{
    T GetTrigger();
}
