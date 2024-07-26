using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Clipboard;

public class SetClipboardText : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Set clipboard text";

    public ActionInput ClipboardText { get; set; }

    public SetClipboardText()
    {
        _cSharpService = new CSharpService();

        ClipboardText = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var clipboardTextValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ClipboardText);

        global::System.Windows.Clipboard.SetText(clipboardTextValue);
    }
}