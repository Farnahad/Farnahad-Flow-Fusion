using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.Clipboard;

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