using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.WindowsServices.WindowsService;

namespace FlowFusion.Action.WindowsServices;

public class StartService : IAction //XXXXXXXXXXXX
{
    private readonly WindowsServiceService _windowsServiceService;

    public string Name => "Start service";

    public ActionInput ServiceToStart { get; set; }

    public StartService()
    {
        _windowsServiceService = new WindowsServiceService();

        ServiceToStart = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var serviceToStart = await sandBox.EvaluateActionInput<string>(ServiceToStart);
        _windowsServiceService.Start(serviceToStart);
    }
}