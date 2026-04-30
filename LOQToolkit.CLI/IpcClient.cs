using System;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;
using LOQToolkit.CLI.Lib;
using LOQToolkit.CLI.Lib.Extensions;
using static LOQToolkit.CLI.Lib.IpcRequest;

namespace LOQToolkit.CLI;

public static class IpcClient
{
    private const int ConnectAttempts = 4;
    private static readonly TimeSpan ConnectTimeout = TimeSpan.FromMilliseconds(500);

    public static Task<string> ListQuickActionsAsync() =>
        QueryAsync(new IpcRequest { Operation = OperationType.ListQuickActions });

    public static Task RunQuickActionAsync(string name) =>
        SendAsync(new IpcRequest { Operation = OperationType.QuickAction, Name = name });

    public static Task<string> ListFeaturesAsync() =>
        QueryAsync(new IpcRequest { Operation = OperationType.ListFeatures });

    public static Task<string> ListFeatureValuesAsync(string name) =>
        QueryAsync(new IpcRequest { Operation = OperationType.ListFeatureValues, Name = name });

    public static Task<string> GetFeatureValueAsync(string name) =>
        QueryAsync(new IpcRequest { Operation = OperationType.GetFeatureValue, Name = name });

    public static Task SetFeatureValueAsync(string name, string value) =>
        SendAsync(new IpcRequest { Operation = OperationType.SetFeatureValue, Name = name, Value = value });

    public static Task<string> GetSpectrumProfileAsync() =>
        QueryAsync(new IpcRequest { Operation = OperationType.GetSpectrumProfile });

    public static Task SetSpectrumProfileAsync(int value) =>
        SendAsync(new IpcRequest { Operation = OperationType.SetSpectrumProfile, Value = value.ToString() });

    public static Task<string> GetSpectrumBrightnessAsync() =>
        QueryAsync(new IpcRequest { Operation = OperationType.GetSpectrumBrightness });

    public static Task SetSpectrumBrightnessAsync(int value) =>
        SendAsync(new IpcRequest { Operation = OperationType.SetSpectrumBrightness, Value = value.ToString() });

    public static Task<string> GetRGBPresetAsync() =>
        QueryAsync(new IpcRequest { Operation = OperationType.GetRGBPreset });

    public static Task SetRGBPresetAsync(int value) =>
        SendAsync(new IpcRequest { Operation = OperationType.SetRGBPreset, Value = value.ToString() });

    private static async Task<string> QueryAsync(IpcRequest req) =>
        await SendRequestAsync(req).ConfigureAwait(false)
        ?? throw new IpcException("Missing return message");

    private static Task SendAsync(IpcRequest req) => SendRequestAsync(req);

    private static async Task<string?> SendRequestAsync(IpcRequest req)
    {
        await using var pipe = new NamedPipeClientStream(Constants.PIPE_NAME);

        await ConnectAsync(pipe).ConfigureAwait(false);

        await pipe.WriteObjectAsync(req).ConfigureAwait(false);
        var res = await pipe.ReadObjectAsync<IpcResponse>().ConfigureAwait(false);

        if (res is null || !res.Success)
            throw new IpcException(res?.Message ?? "Unknown failure");

        return res.Message;
    }

    private static async Task ConnectAsync(NamedPipeClientStream pipe)
    {
        for (var attempt = 0; attempt < ConnectAttempts; attempt++)
        {
            try
            {
                await pipe.ConnectAsync(ConnectTimeout, CancellationToken.None).ConfigureAwait(false);
                pipe.ReadMode = PipeTransmissionMode.Message;
                return;
            }
            catch (TimeoutException) { }
        }

        throw new IpcConnectException();
    }
}
