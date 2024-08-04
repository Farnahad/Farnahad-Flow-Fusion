using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Service.WindowsServices.WindowsService;

namespace FarnahadFlowFusion.Action.WindowsServices;

public class StopService : IAction
{
    private readonly WindowsServiceService _windowsServiceService;

    public string Name => "Stop service";

    public ActionInput ServiceToStop { get; set; }

    public StopService()
    {
        _windowsServiceService = new WindowsServiceService();

        ServiceToStop = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var serviceToStopValue = await sandBox.EvaluateActionInput<string>(ServiceToStop);
        _windowsServiceService.Stop(serviceToStopValue);
    }
}