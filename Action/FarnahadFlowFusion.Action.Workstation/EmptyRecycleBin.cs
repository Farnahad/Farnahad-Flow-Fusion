using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Workstation;

public class EmptyRecycleBin : IAction
{
    private readonly CSharpService _cSharpService;
    private readonly WorkstationService _workstationService;

    public string Name => "Empty recycle bin";

    public EmptyRecycleBin()
    {
        _cSharpService = new CSharpService();
        _workstationService = new WorkstationService();
    }

    public async Task Execute(SandBox sandBox)
    {
        _workstationService.EmptyRecycleBin();
    }
}