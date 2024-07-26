using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Workstation;

public class LockWorkstation : IAction
{
    private readonly CSharpService _cSharpService;
    private readonly WorkstationService _workstationService;

    public string Name => "Lock workstation";

    public LockWorkstation()
    {
        _cSharpService = new CSharpService();
        _workstationService = new WorkstationService();
    }

    public async Task Execute(SandBox sandBox)
    {
        _workstationService.LockUser();
    }
}