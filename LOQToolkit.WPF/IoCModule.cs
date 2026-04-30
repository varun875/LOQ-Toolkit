using Autofac;
using LOQToolkit.Lib.Extensions;
using LOQToolkit.WPF.CLI;
using LOQToolkit.WPF.Settings;
using LOQToolkit.WPF.Utils;

namespace LOQToolkit.WPF;

public class IoCModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register<MainThreadDispatcher>();

        builder.Register<SpectrumScreenCapture>();

        builder.Register<ThemeManager>().AutoActivate();
        builder.Register<NotificationsManager>().AutoActivate();

        builder.Register<DashboardSettings>();

        builder.Register<IpcServer>();
    }
}
