using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Loops;

public class ExitLoop : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Exit loop";

    public ExitLoop()
    {
        _cSharpService = new CSharpService();
    }

    public async Task Execute(SandBox sandBox)
    {
        await Task.CompletedTask;
    }
}