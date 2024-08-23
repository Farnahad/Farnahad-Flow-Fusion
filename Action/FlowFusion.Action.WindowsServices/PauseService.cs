using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.WindowsServices.WindowsService;

namespace FlowFusion.Action.WindowsServices;

public class PauseService(IWindowsServiceService windowsServiceService) : IAction
{
    public string Name => "Pause service";

    public ActionInput ServiceToPause { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var serviceToPauseValue = await sandBox.EvaluateActionInput<string>(ServiceToPause);

        windowsServiceService.PauseService(serviceToPauseValue);
    }
}