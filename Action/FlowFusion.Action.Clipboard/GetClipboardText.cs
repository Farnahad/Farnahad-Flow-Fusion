using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Clipboard;

public class GetClipboardText : IAction //XXXXXXXXXXXX
{

    public string Name => "Get clipboard text";

    public Variable ClipboardText { get; set; }

    public GetClipboardText()
    {
        ClipboardText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        ClipboardText.Value = global::System.Windows.Clipboard.GetText();

        sandBox.SetVariable(ClipboardText);
        await Task.CompletedTask;
    }
}