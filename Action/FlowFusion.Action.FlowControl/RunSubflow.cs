using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.FlowControl;

public class RunSubflow : IAction
{
    public string Name => "Run subflow";

    public WorkFlow WorkFlow { get; set; }

    public RunSubflow()
    {
    }

    public async Task Execute(SandBox sandBox)
    {
        await new SandBox(WorkFlow).Run();
    }
}