using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Service.WindowsServices.WindowsService;

namespace FarnahadFlowFusion.Action.WindowsServices;

public class StartService : IAction
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