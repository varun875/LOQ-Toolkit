using System;
using LOQToolkit.Lib;
using LOQToolkit.Lib.Automation.Steps;
using LOQToolkit.Lib.Listeners;
using LOQToolkit.WPF.Resources;
using LOQToolkit.WPF.Utils;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Automation.Steps;

public class DpiScaleAutomationStepControl : AbstractComboBoxAutomationStepCardControl<DpiScale>
{
    private readonly DisplayConfigurationListener _listener = IoCContainer.Resolve<DisplayConfigurationListener>();

    public DpiScaleAutomationStepControl(IAutomationStep<DpiScale> step) : base(step)
    {
        Icon = SymbolRegular.TextFontSize24;
        Title = Resource.DpiScaleAutomationStepControl_Title;
        Subtitle = Resource.DpiScaleAutomationStepControl_Message;

        _listener.Changed += Listener_Changed;
    }

    protected override string ComboBoxItemDisplayName(DpiScale value)
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
