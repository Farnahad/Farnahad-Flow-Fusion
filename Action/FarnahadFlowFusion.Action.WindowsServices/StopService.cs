using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.WindowsServices;

public class StopService : IAction
{
    private readonly CSharpService _cSharpService;
    private readonly WindowsServiceService _windowsServiceService;

    public string Name => "Stop service";

    public ActionInput ServiceToStop { get; set; }

    public StopService()
    {
        _cSharpService = new CSharpService();
        _windowsServiceService = new WindowsServiceService();

        ServiceToStop = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var serviceToStopValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ServiceToStop);
        _windowsServiceService.Stop(serviceToStopValue);
    }
}