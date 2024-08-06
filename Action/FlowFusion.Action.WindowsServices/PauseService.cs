using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.WindowsServices.WindowsService;

namespace FlowFusion.Action.WindowsServices;

public class PauseService : IAction //XXXXXXXXXXXX
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