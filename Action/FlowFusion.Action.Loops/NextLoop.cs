using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.Loops;

public class NextLoop : IAction
{
    public string Name => "Next loop";

    public NextLoop()
    {
    }

    public async Task Execute(SandBox sandBox)
    {
        await Task.CompletedTask;
    }
}