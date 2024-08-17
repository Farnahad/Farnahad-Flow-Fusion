using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

public class LockWorkstation(IWorkstationService workstationService) : IAction
{
    public string Name => "Lock workstation";

    public async Task Execute(SandBox sandBox)
    {
        workstationService.LockWorkstation();
        await Task.CompletedTask;
    }
}