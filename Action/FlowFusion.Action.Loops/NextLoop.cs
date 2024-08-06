using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Loops;

public class NextLoop : IAction //XXXXXXXXXXXX
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