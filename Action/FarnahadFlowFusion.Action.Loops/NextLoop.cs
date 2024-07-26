using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Loops;

public class NextLoop : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Next loop";

    public NextLoop()
    {
        _cSharpService = new CSharpService();
    }

    public async Task Execute(SandBox sandBox)
    {
        await Task.CompletedTask;
    }
}