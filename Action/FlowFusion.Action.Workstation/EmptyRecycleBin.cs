using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

public class EmptyRecycleBin : IAction //XXXXXXXXXXXX
{
    private readonly WorkstationService _workstationService;

    public string Name => "Empty recycle bin";

    public EmptyRecycleBin()
    {
        _workstationService = new WorkstationService();
    }

    public async Task Execute(SandBox sandBox)
    {
        _workstationService.EmptyRecycleBin();
    }
}