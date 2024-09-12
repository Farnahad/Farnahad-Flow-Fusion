using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.FlowControl;

public class EndRegion : GeneralAction
{
    public override string Name => "End region";

    public EndRegion()
    {
    }

    public override async Task Execute(SandBox sandBox)
    {
        await Task.CompletedTask;
    }
}