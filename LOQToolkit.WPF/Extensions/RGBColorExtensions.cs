using System.Windows.Media;
using LOQToolkit.Lib;

namespace LOQToolkit.WPF.Extensions;

public static class RGBColorExtensions
{
    public static Color ToColor(this RGBColor color) => Color.FromRgb(color.R, color.G, color.B);
}
