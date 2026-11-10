using System.IO;
using System.Text.Json;

namespace ClipNest;

public static class SettingsStore
{
    public static string AppFolder => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ClipNest");
    public static string SettingsPath => Path.Combine(AppFolder, "settings.json");

    public static AppSettings Load()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                var settings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(SettingsPath)) ?? new AppSettings();
                if (string.IsNullOrWhiteSpace(settings.Language) || !LanguageManager.Languages.Any(x => x.Key.Equals(settings.Language, StringComparison.OrdinalIgnoreCase))) settings.Language = "system";
                if (settings.Theme is not ("system" or "light" or "dark")) settings.Theme = "system";
                if (settings.AccentColor is null || !System.Text.RegularExpressions.Regex.IsMatch(settings.AccentColor, "^#[0-9A-Fa-f]{6}$")) settings.AccentColor = "#6750A4";
                settings.MaxHistoryItems = Math.Clamp(settings.MaxHistoryItems, 50, 5000);
                if (settings.PollIntervalMs is not (300 or 500 or 600 or 800 or 1000)) settings.PollIntervalMs = 600;
                return settings;
            }
        }
        catch (Exception)
        {
            // A malformed settings file must not prevent the app from starting.
        }
        return new AppSettings();
    }

    public static void Save(AppSettings settings)
    {
        Directory.CreateDirectory(AppFolder);
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        var tempPath = SettingsPath + ".tmp";
        File.WriteAllText(tempPath, json);
        File.Move(tempPath, SettingsPath, true);
    }
}
