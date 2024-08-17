using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;
using FlowFusion.Service.Workstation.Workstation.Base;

namespace FlowFusion.Action.Workstation;

public class ControlScreenSaver(IWorkstationService workstationService) : IAction
{
    public string Name => "Control screen saver";

    public ScreenSaverAction ScreenSaverAction { get; set; } = ScreenSaverAction.Enable;

    public async Task Execute(SandBox sandBox)
    {
        workstationService.ControlScreenSaver(ScreenSaverAction);

        await Task.CompletedTask;
    }
}