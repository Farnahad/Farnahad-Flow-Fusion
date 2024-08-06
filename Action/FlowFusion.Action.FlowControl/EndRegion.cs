using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.FlowControl;

public class EndRegion : IAction
{
    public string Name => "End region";

    public EndRegion()
    {
    }

    public async Task Execute(SandBox sandBox)
    {
        await Task.CompletedTask;
    }
}