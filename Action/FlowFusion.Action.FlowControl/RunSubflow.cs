using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.FlowControl;

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