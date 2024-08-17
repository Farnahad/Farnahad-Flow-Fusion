using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

public class EmptyRecycleBin(IWorkstationService workstationService) : IAction
{
    public string Name => "Empty recycle bin";

    public async Task Execute(SandBox sandBox)
    {
        workstationService.EmptyRecycleBin();
        await Task.CompletedTask;
    }
}