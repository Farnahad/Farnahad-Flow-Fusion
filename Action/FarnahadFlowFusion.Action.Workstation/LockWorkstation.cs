using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Service.Workstation.Workstation;

namespace FarnahadFlowFusion.Action.Workstation;

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