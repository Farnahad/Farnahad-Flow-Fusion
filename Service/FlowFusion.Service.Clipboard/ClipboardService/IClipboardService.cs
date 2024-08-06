namespace FlowFusion.Service.Clipboard.ClipboardService;

public interface IClipboardService
{
    string GetText();
    void SetText(string text);
    void Clear();
}