using Microsoft.Win32;

namespace ClipNest;

public static class StartupManager
{
    private const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string ValueName = "ClipNest";

    public static bool SetEnabled(bool enabled, bool startMinimized)
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(RunKey, writable: true)
                            ?? Registry.CurrentUser.CreateSubKey(RunKey, writable: true);
            if (key is null) return false;
            if (!enabled)
            {
                key.DeleteValue(ValueName, throwOnMissingValue: false);
                return true;
            }

            var executable = Environment.ProcessPath;
            if (string.IsNullOrWhiteSpace(executable)) return false;
            var command = $"\"{executable}\" --startup" + (startMinimized ? " --minimized" : string.Empty);
            key.SetValue(ValueName, command);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool IsEnabled()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(RunKey, writable: false);
            return key?.GetValue(ValueName) is string;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
