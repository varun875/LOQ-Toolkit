using System;
using LOQToolkit.Lib;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.Lib.Listeners;
using LOQToolkit.WPF.Resources;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class ResolutionAutomationStepControl : AbstractComboBoxAutomationStepCardControl<Resolution>
{
    private readonly DisplayConfigurationListener _listener = IoCContainer.Resolve<DisplayConfigurationListener>();

    public ResolutionAutomationStepControl(IAutomationStep<Resolution> step) : base(step)
    {
        Icon = SymbolRegular.ScaleFill24;
        Title = Resource.ResolutionAutomationStepControl_Title;
        Subtitle = Resource.ResolutionAutomationStepControl_Message;

        _listener.Changed += Listener_Changed;
    }

    private void Listener_Changed(object? sender, EventArgs e) => Dispatcher.Invoke(async () =>
    {
        if (IsLoaded)
            await RefreshAsync();
    });
}
