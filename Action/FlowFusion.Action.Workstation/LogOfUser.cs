using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Service.Workstation.Workstation;

namespace FarnahadFlowFusion.Action.Workstation;

public class LogOfUser : IAction
{
    private readonly WorkstationService _workstationService;

    public string Name => "Log of user";

    public bool Force { get; set; }

    public LogOfUser()
    {
        _workstationService = new WorkstationService();

        Force = false;
    }

    public async Task Execute(SandBox sandBox)
    {
        _workstationService.LogoffUser();
        await Task.CompletedTask;
    }
}