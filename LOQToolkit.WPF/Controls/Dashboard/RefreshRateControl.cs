using System;
using System.Threading.Tasks;
using System.Windows;
using LOQToolkit.Lib;
using LOQToolkit.Lib.Listeners;
using LOQToolkit.WPF.Resources;
using LOQToolkit.WPF.Utils;
using Wpf.Ui.Common;

namespace LOQToolkit.WPF.Controls.Dashboard;

public class RefreshRateControl : AbstractComboBoxFeatureCardControl<RefreshRate>
{
    private readonly DisplayConfigurationListener _listener = IoCContainer.Resolve<DisplayConfigurationListener>();

    public RefreshRateControl()
    {
        Icon = SymbolRegular.DesktopPulse24;
        Title = Resource.RefreshRateControl_Title;
        Subtitle = Resource.RefreshRateControl_Message;

        _listener.Changed += Listener_Changed;
    }

    protected override async Task OnRefreshAsync()
    {
        await base.OnRefreshAsync();

        Visibility = ItemsCount < 2 ? Visibility.Collapsed : Visibility.Visible;
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
