using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Service.WindowsServices.WindowsService;

namespace FarnahadFlowFusion.Action.WindowsServices;

public class PauseService : IAction
{
    private readonly WindowsServiceService _windowsServiceService;

    public string Name => "Pause service";

    public ActionInput ServiceToPause { get; set; }

    public PauseService()
    {
        _windowsServiceService = new WindowsServiceService();

        ServiceToPause = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var serviceToPauseValue = await sandBox.EvaluateActionInput<string>(ServiceToPause);
        _windowsServiceService.Pause(serviceToPauseValue);
    }
}