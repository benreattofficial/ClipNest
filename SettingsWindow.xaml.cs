using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ClipNest;

public partial class SettingsWindow : Window
{
    private readonly AppSettings _original;
    private readonly AppSettings _draft;
    private bool _loading = true;
    private bool _refreshingThemeOptions;

    public SettingsWindow()
    {
        InitializeComponent();
        _original = App.Settings.Copy();
        _draft = App.Settings.Copy();
        StoragePathText.Text = SettingsStore.AppFolder;

        foreach (var language in LanguageManager.Languages)
            LanguageCombo.Items.Add(new ComboBoxItem { Content = language.Key == "system" ? LanguageManager.Get("SystemDefault") : language.Name, Tag = language.Key });
        foreach (var (key, label) in new[] { ("system", "ThemeSystem"), ("light", "ThemeLight"), ("dark", "ThemeDark") })
            ThemeCombo.Items.Add(new ComboBoxItem { Content = LanguageManager.Get(label), Tag = key });
        foreach (var (name, color) in new[]
        {
            ("Indigo", "#6750A4"), ("Blue", "#2563EB"), ("Teal", "#0F9D8A"),
            ("Rose", "#DB2777"), ("Amber", "#EA580C"), ("Green", "#16A34A")
        })
            AccentCombo.Items.Add(new ComboBoxItem { Content = $"{name}  {color}", Tag = color });
        foreach (var milliseconds in new[] { 300, 500, 600, 800, 1000 })
            PollIntervalCombo.Items.Add(new ComboBoxItem { Content = $"{milliseconds} ms", Tag = milliseconds });

        SelectByTag(LanguageCombo, _draft.Language);
        SelectByTag(ThemeCombo, _draft.Theme);
        SelectByTag(AccentCombo, _draft.AccentColor);
        SelectByTag(PollIntervalCombo, _draft.PollIntervalMs);
        StartupCheck.IsChecked = _draft.LaunchAtStartup;
        MinimizedCheck.IsChecked = _draft.StartMinimized;
        MonitorCheck.IsChecked = _draft.MonitorClipboard;
        ConfirmClearCheck.IsChecked = _draft.ConfirmBeforeClear;
        MaxItemsBox.Text = _draft.MaxHistoryItems.ToString();
        _loading = false;
    }

    private static void SelectByTag(ComboBox combo, object value)
    {
        combo.SelectedItem = combo.Items.Cast<ComboBoxItem>().FirstOrDefault(item =>
            string.Equals(Convert.ToString(item.Tag), Convert.ToString(value), StringComparison.OrdinalIgnoreCase));
        if (combo.SelectedItem is null && combo.Items.Count > 0) combo.SelectedIndex = 0;
    }

    private void LanguageCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_loading || LanguageCombo.SelectedItem is not ComboBoxItem { Tag: string language }) return;
        _draft.Language = language;
        Preview();
    }

    private void ThemeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_loading || _refreshingThemeOptions || ThemeCombo.SelectedItem is not ComboBoxItem { Tag: string theme }) return;
        _draft.Theme = theme;
        Preview();
    }

    private void AccentCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_loading || AccentCombo.SelectedItem is not ComboBoxItem { Tag: string color }) return;
        _draft.AccentColor = color;
        Preview();
    }

    private void Preview()
    {
        App.Settings = _draft.Copy();
        LanguageManager.Apply(_draft.Language);
        ThemeManager.Apply(_draft);
        var selectedLanguage = LanguageCombo.SelectedItem as ComboBoxItem;
        if (selectedLanguage?.Tag is string language)
        {
            var themeIndex = ThemeCombo.SelectedIndex;
            _refreshingThemeOptions = true;
            try
            {
                ThemeCombo.Items.Clear();
                foreach (var (key, label) in new[] { ("system", "ThemeSystem"), ("light", "ThemeLight"), ("dark", "ThemeDark") })
                    ThemeCombo.Items.Add(new ComboBoxItem { Content = LanguageManager.Get(label), Tag = key });
                ThemeCombo.SelectedIndex = Math.Clamp(themeIndex, 0, ThemeCombo.Items.Count - 1);
            }
            finally { _refreshingThemeOptions = false; }
        }
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(MaxItemsBox.Text, out var maxItems) || maxItems is < 50 or > 5000)
        {
            StatusText.Text = LanguageManager.Get("MaxHistory") + ": 50–5000";
            MaxItemsBox.Focus();
            MaxItemsBox.SelectAll();
            return;
        }

        _draft.LaunchAtStartup = StartupCheck.IsChecked == true;
        _draft.StartMinimized = MinimizedCheck.IsChecked == true;
        _draft.MonitorClipboard = MonitorCheck.IsChecked == true;
        _draft.ConfirmBeforeClear = ConfirmClearCheck.IsChecked == true;
        _draft.MaxHistoryItems = maxItems;
        if (PollIntervalCombo.SelectedItem is ComboBoxItem { Tag: int interval }) _draft.PollIntervalMs = interval;
        App.Settings = _draft.Copy();

        try { SettingsStore.Save(App.Settings); }
        catch (Exception)
        {
            StatusText.Text = LanguageManager.Get("SaveError");
            return;
        }

        var startupUpdated = StartupManager.SetEnabled(App.Settings.LaunchAtStartup, App.Settings.StartMinimized);
        if (!startupUpdated && App.Settings.LaunchAtStartup)
        {
            StatusText.Text = LanguageManager.Get("StartupError");
            return;
        }
        LanguageManager.Apply(App.Settings.Language);
        ThemeManager.Apply(App.Settings);
        DialogResult = true;
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        RestoreOriginalSettings();
        DialogResult = false;
        Close();
    }

    private void RestoreOriginalSettings()
    {
        App.Settings = _original.Copy();
        LanguageManager.Apply(App.Settings.Language);
        ThemeManager.Apply(App.Settings);
    }

    protected override void OnClosed(EventArgs e)
    {
        if (DialogResult != true) RestoreOriginalSettings();
        base.OnClosed(e);
    }

    private void OpenFolder_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Directory.CreateDirectory(SettingsStore.AppFolder);
            Process.Start(new ProcessStartInfo("explorer.exe", SettingsStore.AppFolder) { UseShellExecute = true });
        }
        catch (Exception)
        {
            StatusText.Text = LanguageManager.Get("SaveError");
        }
    }
}
