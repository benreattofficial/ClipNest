using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace ClipNest;

public partial class MainWindow : Window
{
    private const int MaxTextLength = 25_000;
    private readonly ObservableCollection<ClipboardEntry> _entries = [];
    private readonly ICollectionView _view;
    private readonly DispatcherTimer _timer;
    private string _lastClipboardText = string.Empty;
    private bool _monitoring;

    public bool StartMinimized
    {
        get => WindowState == WindowState.Minimized;
        set { if (value) WindowState = WindowState.Minimized; }
    }

    private string HistoryPath => Path.Combine(SettingsStore.AppFolder, "history.json");

    public MainWindow()
    {
        InitializeComponent();
        try { if (Clipboard.ContainsText()) _lastClipboardText = Clipboard.GetText(); } catch (Exception) { }
        _monitoring = App.Settings.MonitorClipboard;
        _view = CollectionViewSource.GetDefaultView(_entries);
        _view.SortDescriptions.Add(new SortDescription(nameof(ClipboardEntry.IsPinned), ListSortDirection.Descending));
        _view.SortDescriptions.Add(new SortDescription(nameof(ClipboardEntry.DateAdded), ListSortDirection.Descending));
        _view.Filter = FilterEntry;
        HistoryList.ItemsSource = _view;
        LoadHistory();
        ApplyLanguage();
        UpdateMonitoringState();
        RefreshView();

        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(App.Settings.PollIntervalMs) };
        _timer.Tick += CheckClipboard;
        _timer.Start();
    }

    private static string L(string key) => LanguageManager.Get(key);

    private void ApplyLanguage()
    {
        SubtitleTextFallback();
        if (MonitoringLabel is not null)
            MonitoringLabel.Text = _monitoring ? L("MonitoringOn") : L("MonitoringPaused");
        if (ToggleMonitoringButton is not null)
            ToggleMonitoringButton.Content = _monitoring ? L("Pause") : L("Resume");
        if (CountLabel is not null) UpdateLabels();
    }

    private void SubtitleTextFallback() { }

    private void CheckClipboard(object? sender, EventArgs e)
    {
        if (!_monitoring) return;
        try
        {
            if (!Clipboard.ContainsText()) return;
            var text = Clipboard.GetText();
            if (string.IsNullOrWhiteSpace(text) || text.Length > MaxTextLength || text == _lastClipboardText) return;
            _lastClipboardText = text;
            if (_entries.FirstOrDefault(item => item.Text == text) is { } duplicate)
            {
                _entries.Remove(duplicate);
                duplicate.DateAdded = DateTime.Now;
                _entries.Insert(0, duplicate);
            }
            else
            {
                _entries.Insert(0, new ClipboardEntry { Text = text, DateAdded = DateTime.Now });
            }
            TrimHistory();
            RefreshView();
            SaveHistory();
            StatusLabel.Text = L("NewEntry");
        }
        catch (Exception)
        {
            // Clipboard can be temporarily locked by another application; retry on the next timer tick.
        }
    }

    private bool FilterEntry(object item)
    {
        if (item is not ClipboardEntry entry) return false;
        var query = SearchBox?.Text?.Trim();
        return string.IsNullOrEmpty(query) || entry.Text.Contains(query, StringComparison.CurrentCultureIgnoreCase);
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) => RefreshView();

    private void ToggleMonitoring_Click(object sender, RoutedEventArgs e)
    {
        _monitoring = !_monitoring;
        UpdateMonitoringState();
        StatusLabel.Text = _monitoring ? L("MonitoringOn") : L("MonitoringPaused");
    }

    private void UpdateMonitoringState()
    {
        if (MonitoringLabel is null) return;
        MonitoringLabel.Text = _monitoring ? L("MonitoringOn") : L("MonitoringPaused");
        ToggleMonitoringButton.Content = _monitoring ? L("Pause") : L("Resume");
        StatusDot.Fill = (Brush)Application.Current.Resources[_monitoring ? "SuccessBrush" : "MutedBrush"];
    }

    private void CopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: ClipboardEntry entry }) return;
        try
        {
            Clipboard.SetText(entry.Text);
            _lastClipboardText = entry.Text;
            StatusLabel.Text = L("Copied");
        }
        catch (Exception) { StatusLabel.Text = L("ClipboardError"); }
    }

    private void PinButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: ClipboardEntry entry }) return;
        entry.IsPinned = !entry.IsPinned;
        RefreshView();
        SaveHistory();
        StatusLabel.Text = entry.IsPinned ? L("PinnedMarker") : L("Unpin");
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: ClipboardEntry entry }) return;
        _entries.Remove(entry);
        SaveHistory();
        RefreshView();
        StatusLabel.Text = L("Delete");
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        var unpinned = _entries.Where(entry => !entry.IsPinned).ToList();
        if (unpinned.Count == 0) return;
        if (App.Settings.ConfirmBeforeClear)
        {
            var result = MessageBox.Show(this, L("ConfirmClearMessage"), L("ConfirmClearTitle"),
                MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (result != MessageBoxResult.Yes) return;
        }
        foreach (var entry in unpinned) _entries.Remove(entry);
        SaveHistory();
        RefreshView();
        StatusLabel.Text = L("ClearUnpinned");
    }

    private void OpenSettings_Click(object sender, RoutedEventArgs e)
    {
        var previousPreference = App.Settings.MonitorClipboard;
        var previousRunningState = _monitoring;
        var dialog = new SettingsWindow { Owner = this };
        if (dialog.ShowDialog() != true) return;
        _monitoring = App.Settings.MonitorClipboard == previousPreference ? previousRunningState : App.Settings.MonitorClipboard;
        _timer.Interval = TimeSpan.FromMilliseconds(App.Settings.PollIntervalMs);
        TrimHistory();
        ApplyLanguage();
        UpdateMonitoringState();
        RefreshView();
        StatusLabel.Text = L("SettingsSaved");
    }

    private void LoadHistory()
    {
        try
        {
            if (!File.Exists(HistoryPath)) return;
            var entries = JsonSerializer.Deserialize<List<ClipboardEntry>>(File.ReadAllText(HistoryPath)) ?? [];
            foreach (var item in entries.OrderByDescending(entry => entry.IsPinned).ThenByDescending(entry => entry.DateAdded)
                         .Take(Math.Clamp(App.Settings.MaxHistoryItems, 50, 5000)))
                _entries.Add(item);
        }
        catch (Exception)
        {
            StatusLabel.Text = L("HistoryReadError");
        }
    }

    private void SaveHistory()
    {
        try
        {
            Directory.CreateDirectory(SettingsStore.AppFolder);
            var tempPath = HistoryPath + ".tmp";
            File.WriteAllText(tempPath, JsonSerializer.Serialize(_entries));
            File.Move(tempPath, HistoryPath, true);
        }
        catch (Exception) { StatusLabel.Text = L("SaveError"); }
    }

    private void TrimHistory()
    {
        var maxItems = Math.Clamp(App.Settings.MaxHistoryItems, 50, 5000);
        while (_entries.Count > maxItems)
        {
            var oldestUnpinned = _entries.Where(entry => !entry.IsPinned).MinBy(entry => entry.DateAdded);
            if (oldestUnpinned is null) break;
            _entries.Remove(oldestUnpinned);
        }
        SaveHistory();
    }

    private void RefreshView()
    {
        _view.Refresh();
        UpdateLabels();
    }

    private void UpdateLabels()
    {
        CountLabel.Text = $"{L("History")}  ·  {_entries.Count} {L("Items")}  ·  {_entries.Count(entry => entry.IsPinned)} {L("PinnedCount")}";
        EmptyState.Visibility = _entries.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        _timer.Stop();
        SaveHistory();
        base.OnClosing(e);
    }
}
