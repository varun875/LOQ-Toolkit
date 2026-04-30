using System;
using LOQToolkit.Lib;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.Lib.Listeners;
using LOQToolkit.WPF.Resources;
using LOQToolkit.WPF.Utils;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class RefreshRateAutomationStepControl : AbstractComboBoxAutomationStepCardControl<RefreshRate>
{
    private readonly DisplayConfigurationListener _listener = IoCContainer.Resolve<DisplayConfigurationListener>();

    public RefreshRateAutomationStepControl(IAutomationStep<RefreshRate> step) : base(step)
    {
        Icon = SymbolRegular.DesktopPulse24;
        Title = Resource.RefreshRateAutomationStepControl_Title;
        Subtitle = Resource.RefreshRateAutomationStepControl_Message;

        _listener.Changed += Listener_Changed;
    }

    protected override string ComboBoxItemDisplayName(RefreshRate value)
    {
        var str = base.ComboBoxItemDisplayName(value);
        return LocalizationHelper.ForceLeftToRight(str);
    }

    private void Listener_Changed(object? sender, EventArgs e) => Dispatcher.Invoke(async () =>
    {
        if (IsLoaded)
            await RefreshAsync();
    });
}
