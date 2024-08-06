using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Clipboard.ClipboardService;

namespace FlowFusion.Action.Clipboard;

public class ClearClipboardContents(IClipboardService clipboardService) : IAction
{
    public string Name => "Clear clipboard contents";

    public async Task Execute(SandBox sandBox)
    {
        clipboardService.Clear();
        await Task.CompletedTask;
    }
}