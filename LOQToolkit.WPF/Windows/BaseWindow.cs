using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace LOQToolkit.WPF.Windows;

public class BaseWindow : UiWindow
{
    private const int DWMWA_SYSTEMBACKDROP_TYPE = 38;
    private const int DWMSBT_MAINWINDOW = 2;
    private const int DWMSBT_TRANSIENTWINDOW = 3;

    protected BaseWindow()
    {
        SnapsToDevicePixels = true;
        ExtendsContentIntoTitleBar = true;

        WindowBackdropType = BackgroundType.Mica;

        SourceInitialized += OnSourceInitialized;
        DpiChanged += BaseWindow_DpiChanged;
    }

    private void OnSourceInitialized(object? sender, EventArgs e)
    {
        if (Environment.OSVersion.Version.Build < 22000)
            return;

        var hwnd = new WindowInteropHelper(this).Handle;
        var backdrop = DWMSBT_MAINWINDOW;
        DwmSetWindowAttribute(hwnd, DWMWA_SYSTEMBACKDROP_TYPE, ref backdrop, Marshal.SizeOf(typeof(int)));
    }

    private void BaseWindow_DpiChanged(object sender, DpiChangedEventArgs e) => VisualTreeHelper.SetRootDpi(this, e.NewDpi);

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
}
