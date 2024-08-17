using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

public class LogOfUser(IWorkstationService workstationService) : IAction
{
    public string Name => "Log of user";

    public bool Force { get; set; } = false;

    public async Task Execute(SandBox sandBox)
    {
        workstationService.LogOfUser(Force);
        await Task.CompletedTask;
    }
}