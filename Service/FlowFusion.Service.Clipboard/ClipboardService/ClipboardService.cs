namespace FlowFusion.Service.Clipboard.ClipboardService;

public class ClipboardService : IClipboardService
{
    public string GetText()
    {
        return global::System.Windows.Clipboard.GetText();
    }

    public void SetText(string text)
    {
        global::System.Windows.Clipboard.SetText(text);
    }

    public void Clear()
    {
        global::System.Windows.Clipboard.Clear();
    }
}