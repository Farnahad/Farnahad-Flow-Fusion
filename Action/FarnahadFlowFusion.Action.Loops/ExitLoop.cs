using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.Loops;

public class ExitLoop : IAction
{
    public string Name => "Exit loop";

    public ExitLoop()
    {
    }

    public async Task Execute(SandBox sandBox)
    {
        await Task.CompletedTask;
    }
}