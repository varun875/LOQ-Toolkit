using System;

namespace LOQToolkit.CLI.Lib;

public class IpcException(string? message) : Exception(message);
