using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;
using FlowFusion.Service.Workstation.Workstation.Base;

namespace FlowFusion.Action.Workstation;

public class ShowDesktop(IWorkstationService workstationService) : IAction
{
    public string Name => "Show desktop";

    public ShowDesktopOperation Operation { get; set; } = ShowDesktopOperation.MinimizeAllWindowsShowDesktop;

    public async Task Execute(SandBox sandBox)
    {
        workstationService.ShowDesktop(Operation);
        await Task.CompletedTask;
    }
}