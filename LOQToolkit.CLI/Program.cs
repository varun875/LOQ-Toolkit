using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.CommandLine.Parsing;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LOQToolkit.CLI.Lib;

namespace LOQToolkit.CLI;

public static class Program
{
    private const int ExitCodeConnectError = 1;
    private const int ExitCodeIpcError = 2;
    private const int ExitCodeUnknown = 99;

    private const string AppName = "LOQ Toolkit CLI";

    public static Task<int> Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        return BuildCommandLine().InvokeAsync(args);
    }

    private static Parser BuildCommandLine()
    {
        var root = new RootCommand(
            "Control LOQ Toolkit hardware features from the command line.\n" +
            "LOQ Toolkit must be running in background with CLI enabled in Settings.")
        {
            BuildQuickActionsCommand(),
            BuildFeatureCommand(),
            BuildSpectrumCommand(),
            BuildRGBCommand()
        };

        return new CommandLineBuilder(root)
            .UseDefaults()
            .UseHelp(ctx => ctx.HelpBuilder.CustomizeLayout(BuildHelpLayout))
            .UseExceptionHandler(OnException)
            .Build();
    }

    private static IEnumerable<HelpSectionDelegate> BuildHelpLayout(HelpContext context)
    {
        var isRoot = context.Command is RootCommand;

        if (isRoot)
            yield return WriteBanner;

        yield return HelpBuilder.Default.SynopsisSection();
        yield return HelpBuilder.Default.CommandUsageSection();
        yield return HelpBuilder.Default.CommandArgumentsSection();
        yield return HelpBuilder.Default.OptionsSection();
        yield return HelpBuilder.Default.SubcommandsSection();
        yield return HelpBuilder.Default.AdditionalArgumentsSection();

        if (isRoot)
            yield return WriteExamples;
    }

    private static void WriteBanner(HelpContext context)
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "1.0.0";
        var inside = $"  {AppName}{new string(' ', Math.Max(1, 25 - version.Length))}v{version}  ";
        var border = new string('─', inside.Length);
        var w = context.Output;
        var useColor = !Console.IsOutputRedirected;

        if (useColor) Console.ForegroundColor = ConsoleColor.Cyan;
        w.WriteLine($"  ╭{border}╮");
        w.WriteLine($"  │{inside}│");
        w.WriteLine($"  ╰{border}╯");
        if (useColor) Console.ResetColor();
    }

    private static void WriteExamples(HelpContext context)
    {
        var w = context.Output;
        w.WriteLine();
        w.WriteLine("Examples:");
        w.WriteLine("  loq-toolkit quickAction --list");
        w.WriteLine("  loq-toolkit feature get power-mode");
        w.WriteLine("  loq-toolkit spectrum brightness set 80");
        w.WriteLine("  loq-toolkit rgb set 1");
    }

    private static Command BuildQuickActionsCommand()
    {
        var nameArgument = new Argument<string>("name", "Name of the Quick Action") { Arity = ArgumentArity.ZeroOrOne };
        var listOption = new Option<bool>(["--list", "-l"], "List available Quick Actions") { Arity = ArgumentArity.ZeroOrOne };

        var cmd = new Command("quickAction", "Run Quick Action");
        cmd.AddAlias("qa");
        cmd.AddArgument(nameArgument);
        cmd.AddOption(listOption);
        cmd.SetHandler(async (name, list) =>
        {
            if (list)
                Console.WriteLine(await IpcClient.ListQuickActionsAsync());
            else
                await IpcClient.RunQuickActionAsync(name);
        }, nameArgument, listOption);
        cmd.AddValidator(result =>
        {
            if (result.FindResultFor(nameArgument) is null && result.FindResultFor(listOption) is null)
                result.ErrorMessage = $"{nameArgument.Name} or --{listOption.Name} should be specified";
        });

        return cmd;
    }

    private static Command BuildFeatureCommand()
    {
        var getCmd = BuildGetFeatureCommand();
        var setCmd = BuildSetFeatureCommand();
        var listOption = new Option<bool>(["--list", "-l"], "List available features") { Arity = ArgumentArity.ZeroOrOne };

        var cmd = new Command("feature", "Control features")
        {
            getCmd,
            setCmd
        };
        cmd.AddAlias("f");
        cmd.AddOption(listOption);
        cmd.SetHandler(async list =>
        {
            if (list)
                Console.WriteLine(await IpcClient.ListFeaturesAsync());
        }, listOption);
        cmd.AddValidator(result =>
        {
            if (result.FindResultFor(getCmd) is null
                && result.FindResultFor(setCmd) is null
                && result.FindResultFor(listOption) is null)
            {
                result.ErrorMessage = $"{getCmd.Name}, {setCmd.Name} or --{listOption.Name} should be specified";
            }
        });

        return cmd;
    }

    private static Command BuildGetFeatureCommand()
    {
        var nameArgument = new Argument<string>("name", "Name of the feature") { Arity = ArgumentArity.ExactlyOne };

        var cmd = new Command("get", "Get value of a feature");
        cmd.AddAlias("g");
        cmd.AddArgument(nameArgument);
        cmd.SetHandler(async name => Console.WriteLine(await IpcClient.GetFeatureValueAsync(name)), nameArgument);

        return cmd;
    }

    private static Command BuildSetFeatureCommand()
    {
        var nameArgument = new Argument<string>("name", "Name of the feature") { Arity = ArgumentArity.ExactlyOne };
        var valueArgument = new Argument<string>("value", "Value of the feature") { Arity = ArgumentArity.ZeroOrOne };
        var listOption = new Option<bool>(["--list", "-l"], "List available feature values") { Arity = ArgumentArity.ZeroOrOne };

        var cmd = new Command("set", "Set value of a feature");
        cmd.AddAlias("s");
        cmd.AddArgument(nameArgument);
        cmd.AddArgument(valueArgument);
        cmd.AddOption(listOption);
        cmd.SetHandler(async (name, value, list) =>
        {
            if (list)
                Console.WriteLine(await IpcClient.ListFeatureValuesAsync(name));
            else
                await IpcClient.SetFeatureValueAsync(name, value);
        }, nameArgument, valueArgument, listOption);
        cmd.AddValidator(result =>
        {
            if (result.FindResultFor(nameArgument) is null && result.FindResultFor(listOption) is null)
                result.ErrorMessage = $"{nameArgument.Name} or --{listOption.Name} should be specified";
        });

        return cmd;
    }

    private static Command BuildSpectrumCommand()
    {
        var cmd = new Command("spectrum", "Control Spectrum backlight")
        {
            BuildIntGetSetCommand(
                "profile", "p",
                "Control Spectrum backlight profile",
                "profile", "Profile to set",
                IpcClient.GetSpectrumProfileAsync,
                IpcClient.SetSpectrumProfileAsync),
            BuildIntGetSetCommand(
                "brightness", "b",
                "Control Spectrum brightness",
                "brightness", "Brightness to set",
                IpcClient.GetSpectrumBrightnessAsync,
                IpcClient.SetSpectrumBrightnessAsync)
        };
        cmd.AddAlias("s");
        return cmd;
    }

    private static Command BuildRGBCommand() =>
        BuildIntGetSetCommand(
            "rgb", "r",
            "Control RGB backlight preset",
            "preset", "Preset to set",
            IpcClient.GetRGBPresetAsync,
            IpcClient.SetRGBPresetAsync);

    private static Command BuildIntGetSetCommand(
        string name,
        string alias,
        string description,
        string argName,
        string argDescription,
        Func<Task<string>> getter,
        Func<int, Task> setter)
    {
        var getCmd = new Command("get", $"Get current {argName}");
        getCmd.AddAlias("g");
        getCmd.SetHandler(async _ => Console.WriteLine(await getter()));

        var valueArgument = new Argument<int>(argName, argDescription) { Arity = ArgumentArity.ExactlyOne };
        var setCmd = new Command("set", $"Set current {argName}");
        setCmd.AddAlias("s");
        setCmd.AddArgument(valueArgument);
        setCmd.SetHandler(async value => await setter(value), valueArgument);

        var cmd = new Command(name, description) { getCmd, setCmd };
        cmd.AddAlias(alias);
        return cmd;
    }

    private static void OnException(Exception ex, InvocationContext context)
    {
        var (message, exitCode) = ex switch
        {
            IpcConnectException => ("Failed to connect. " +
                                    "Make sure that LOQ Toolkit is running " +
                                    "in background and CLI is enabled in Settings.", ExitCodeConnectError),
            IpcException => (ex.Message, ExitCodeIpcError),
            _ => (ex.ToString(), ExitCodeUnknown)
        };

        var useColor = !Console.IsErrorRedirected;
        if (useColor)
            Console.ForegroundColor = ConsoleColor.Red;

        context.Console.Error.WriteLine(message);
        context.ExitCode = exitCode;

        if (useColor)
            Console.ResetColor();
    }
}
