using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.FlowControl;

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