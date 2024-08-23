using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.WindowsServices.WindowsService;

namespace FlowFusion.Action.WindowsServices;

public class StopService(IWindowsServiceService windowsServiceService) : IAction
{
    public string Name => "Stop service";

    public ActionInput ServiceToStop { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var serviceToStopValue = await sandBox.EvaluateActionInput<string>(ServiceToStop);

        windowsServiceService.StopService(serviceToStopValue);
    }
}