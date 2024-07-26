using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.FlowControl;

public class RunSubflow : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Run subflow";

    public WorkFlow WorkFlow { get; set; }

    public RunSubflow()
    {
        _cSharpService = new CSharpService();
    }

    public async Task Execute(SandBox sandBox)
    {
        await new SandBox(WorkFlow).Run();
    }
}