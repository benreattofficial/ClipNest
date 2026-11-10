namespace ClipNest;

public sealed class ClipboardEntry
{
    public string Text { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; } = DateTime.Now;
    public bool IsPinned { get; set; }
}
