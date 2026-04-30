using System.Threading.Tasks;
using System.Windows;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class TurnOffWiFiAutomationStepControl : AbstractAutomationStepControl
{
    public TurnOffWiFiAutomationStepControl(TurnOffWiFiAutomationStep automationStep) : base(automationStep)
    {
        Icon = SymbolRegular.WifiOff24;
        Title = Resource.TurnOffWiFiAutomationStepControl_Title;
    }

    public override IAutomationStep CreateAutomationStep() => new TurnOffWiFiAutomationStep();

    protected override UIElement? GetCustomControl() => null;

    protected override void OnFinishedLoading() { }

    protected override Task RefreshAsync() => Task.CompletedTask;
}
