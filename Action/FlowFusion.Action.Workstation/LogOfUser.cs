using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

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