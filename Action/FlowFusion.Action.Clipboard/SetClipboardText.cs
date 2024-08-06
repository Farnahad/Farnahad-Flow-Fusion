using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Clipboard.ClipboardService;

namespace FlowFusion.Action.Clipboard;

public class SetClipboardText(IClipboardService clipboardService) : IAction
{
    private readonly IClipboardService _clipboardService = clipboardService;

    public string Name => "Set clipboard text";

    public ActionInput ClipboardText { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var clipboardTextValue = await sandBox.EvaluateActionInput<string>(ClipboardText);

        _clipboardService.SetText(clipboardTextValue);
    }
}