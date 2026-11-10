using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;

namespace ClipNest;

public static class ThemeManager
{
    public static void Apply(AppSettings settings)
    {
        var dark = settings.Theme.Equals("dark", StringComparison.OrdinalIgnoreCase) ||
                   (settings.Theme.Equals("system", StringComparison.OrdinalIgnoreCase) && IsSystemDark());
        var background = Parse(dark ? "#171923" : "#F4F6FA");
        var surface = Parse(dark ? "#222531" : "#FFFFFF");
        var text = Parse(dark ? "#EDF0F7" : "#172033");
        var muted = Parse(dark ? "#A7ADBC" : "#697386");
        var border = Parse(dark ? "#383D4D" : "#E3E7EF");
        var accent = Parse(settings.AccentColor);
        var soft = Blend(accent, dark ? Parse("#222531") : Parse("#FFFFFF"), dark ? 0.76 : 0.88);

        Set("WindowBackgroundBrush", background);
        Set("SurfaceBrush", surface);
        Set("TextBrush", text);
        Set("MutedBrush", muted);
        Set("BorderBrush", border);
        Set("AccentBrush", accent);
        Set("AccentSoftBrush", soft);
        Set("DangerBrush", Parse(dark ? "#FF7777" : "#B42318"));
        Set("SuccessBrush", Parse(dark ? "#6DD6A3" : "#18794E"));
    }

    private static bool IsSystemDark()
    {
        try
        {
            var value = Registry.GetValue(
                @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
                "AppsUseLightTheme", 1);
            return value is int setting && setting == 0;
        }
        catch (Exception) { return false; }
    }

    private static Color Parse(string value) => (Color)ColorConverter.ConvertFromString(value)!;

    private static Color Blend(Color foreground, Color background, double backgroundWeight)
    {
        var foregroundWeight = 1 - backgroundWeight;
        return Color.FromRgb(
            (byte)(foreground.R * foregroundWeight + background.R * backgroundWeight),
            (byte)(foreground.G * foregroundWeight + background.G * backgroundWeight),
            (byte)(foreground.B * foregroundWeight + background.B * backgroundWeight));
    }

    private static void Set(string key, Color value)
    {
        Application.Current.Resources[key] = new SolidColorBrush(value);
    }
}
