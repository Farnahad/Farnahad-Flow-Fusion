using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Clipboard.ClipboardService;

namespace FlowFusion.Action.Clipboard;

public class GetClipboardText(IClipboardService clipboardService) : IAction
{
    public string Name => "Get clipboard text";

    public Variable ClipboardText { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        ClipboardText.Value = clipboardService.GetText();

        sandBox.SetVariable(ClipboardText);
        await Task.CompletedTask;
    }
}