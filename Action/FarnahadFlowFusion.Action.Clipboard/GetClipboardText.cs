using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Clipboard;

public class GetClipboardText : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Get clipboard text";

    public Variable ClipboardText { get; set; }

    public GetClipboardText()
    {
        _cSharpService = new CSharpService();

        ClipboardText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        ClipboardText.Value = global::System.Windows.Clipboard.GetText();

        sandBox.Variables.Add(ClipboardText);
        await Task.CompletedTask;
    }
}