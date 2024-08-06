using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Loops;

public class ExitLoop : IAction //XXXXXXXXXXXX
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