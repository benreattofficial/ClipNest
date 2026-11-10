namespace ClipNest;

public sealed class AppSettings
{
    public string Language { get; set; } = "system";
    public string Theme { get; set; } = "system";
    public string AccentColor { get; set; } = "#6750A4";
    public bool LaunchAtStartup { get; set; }
    public bool StartMinimized { get; set; }
    public bool MonitorClipboard { get; set; } = true;
    public bool ConfirmBeforeClear { get; set; } = true;
    public int MaxHistoryItems { get; set; } = 300;
    public int PollIntervalMs { get; set; } = 600;

    public AppSettings Copy() => (AppSettings)MemberwiseClone();
}
