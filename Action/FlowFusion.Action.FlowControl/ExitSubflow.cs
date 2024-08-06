using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.FlowControl;

public class ExitSubflow : IAction
{
    public string Name => "Exit Subflow";

    public ExitSubflow()
    {
    }

    public async Task Execute(SandBox sandBox)
    {
        sandBox.SandBoxStatus = SandBoxStatus.Stopping;
        await Task.CompletedTask;
    }
}