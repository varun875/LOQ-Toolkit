using System;

namespace LOQToolkit.Lib.PackageDownloader;

public class UpdateCatalogNotFoundException(string? message, Exception? ex) : Exception(message, ex);
