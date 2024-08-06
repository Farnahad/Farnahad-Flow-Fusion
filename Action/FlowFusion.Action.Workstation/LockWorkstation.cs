using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

public class LockWorkstation : IAction
{
    private readonly WorkstationService _workstationService;

    public string Name => "Lock workstation";

    public LockWorkstation()
    {
        _workstationService = new WorkstationService();
    }

    public async Task Execute(SandBox sandBox)
    {
        _workstationService.LockUser();
    }
}