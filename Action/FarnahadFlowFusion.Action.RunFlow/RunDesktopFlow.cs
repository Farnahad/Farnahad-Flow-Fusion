using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.RunFlow;

public class RunDesktopFlow : IAction
{
    public string Name => "Run desktop flow";

    public WorkFlow WorkFlow { get; set; }

    public RunDesktopFlow()
    {
    }

    public async Task Execute(SandBox sandBox)
    {
        await new SandBox(WorkFlow).Run();
    }
}