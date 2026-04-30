using Autofac;
using LOQToolkit.Lib.Automation.Utils;
using LOQToolkit.Lib.Extensions;

namespace LOQToolkit.Lib.Automation;

public class IoCModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register<AutomationSettings>();
        builder.Register<AutomationProcessor>();
    }
}
