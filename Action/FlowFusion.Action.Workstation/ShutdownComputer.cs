using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;
using FlowFusion.Service.Workstation.Workstation.Base;

namespace FlowFusion.Action.Workstation;

public class ShutdownComputer(IWorkstationService workstationService) : IAction
{
    public string Name => "Shutdown computer";

    public ShutdownComputerActionToPerform ActionToPerform { get; set; } = ShutdownComputerActionToPerform.Shutdown;
    public bool Force { get; set; } = false;

    public async Task Execute(SandBox sandBox)
    {
        await workstationService.ShutdownComputer(ActionToPerform, Force);
    }
}