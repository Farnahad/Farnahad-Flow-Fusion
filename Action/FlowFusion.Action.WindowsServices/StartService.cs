using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.WindowsServices.WindowsService;

namespace FlowFusion.Action.WindowsServices;

public class StartService(IWindowsServiceService windowsServiceService) : IAction
{
    public string Name => "Start service";

    public ActionInput ServiceToStart { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var serviceToStart = await sandBox.EvaluateActionInput<string>(ServiceToStart);

        windowsServiceService.StartService(serviceToStart);
    }
}