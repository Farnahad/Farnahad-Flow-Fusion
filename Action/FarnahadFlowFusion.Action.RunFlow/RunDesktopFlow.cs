using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.RunFlow;

public class RunDesktopFlow : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Run desktop flow";

    public WorkFlow WorkFlow { get; set; }

    public RunDesktopFlow()
    {
        _cSharpService = new CSharpService();
    }

    public async Task Execute(SandBox sandBox)
    {
        await new SandBox(WorkFlow).Run();
    }
}