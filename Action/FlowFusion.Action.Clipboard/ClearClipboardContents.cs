using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Clipboard;

public class ClearClipboardContents : IAction
{
    public string Name => "Clear clipboard contents";

    public ClearClipboardContents()
    {
    }

    public async Task Execute(SandBox sandBox)
    {
        global::System.Windows.Clipboard.Clear();
        await Task.CompletedTask;
    }
}