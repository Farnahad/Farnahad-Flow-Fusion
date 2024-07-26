using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Workstation;

public class LogOfUser : IAction
{
    private readonly CSharpService _cSharpService;
    private readonly WorkstationService _workstationService;

    public string Name => "Log of user";

    public bool Force { get; set; }

    public LogOfUser()
    {
        _cSharpService = new CSharpService();
        _workstationService = new WorkstationService();

        Force = false;
    }

    public async Task Execute(SandBox sandBox)
    {
        _workstationService.LogoffUser();
        await Task.CompletedTask;
    }
}