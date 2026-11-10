using System.Windows;

namespace ClipNest;

public partial class App : Application
{
    public static AppSettings Settings { get; internal set; } = new();

    protected override void OnStartup(StartupEventArgs e)
    {
        Settings = SettingsStore.Load();
        LanguageManager.Apply(Settings.Language);
        ThemeManager.Apply(Settings);
        StartupManager.SetEnabled(Settings.LaunchAtStartup, Settings.StartMinimized);
        base.OnStartup(e);

        var mainWindow = new MainWindow { StartMinimized = e.Args.Contains("--minimized", StringComparer.OrdinalIgnoreCase) };
        MainWindow = mainWindow;
        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        try { SettingsStore.Save(Settings); } catch (Exception) { }
        base.OnExit(e);
    }
}
