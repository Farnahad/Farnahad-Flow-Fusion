using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Clipboard;

public class ClearClipboardContents : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Clear clipboard contents";

    public ClearClipboardContents()
    {
        _cSharpService = new CSharpService();
    }

    public async Task Execute(SandBox sandBox)
    {
        global::System.Windows.Clipboard.Clear();
        await Task.CompletedTask;
    }
}