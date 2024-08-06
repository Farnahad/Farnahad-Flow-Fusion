using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.FlowControl;

public class End : IAction //XXXXXXXXXXXX
{
    public string Name => "End";

    public End()
    {
    }

    public async Task Execute(SandBox sandBox)
    {
        sandBox.SandBoxStatus = SandBoxStatus.Stopping;
        await Task.CompletedTask;
    }
}