using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Clipboard;

public class GetClipboardText : IAction
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