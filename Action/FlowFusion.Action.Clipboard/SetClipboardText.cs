using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Clipboard;

public class SetClipboardText : IAction
{
    public string Name => "Set clipboard text";

    public ActionInput ClipboardText { get; set; }

    public SetClipboardText()
    {
        ClipboardText = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var clipboardTextValue = await sandBox.EvaluateActionInput<string>(ClipboardText);

        global::System.Windows.Clipboard.SetText(clipboardTextValue);
    }
}