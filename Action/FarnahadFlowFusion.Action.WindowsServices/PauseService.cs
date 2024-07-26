using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.WindowsServices;

public class PauseService : IAction
{
    private readonly CSharpService _cSharpService;
    private readonly WindowsServiceService _windowsServiceService;

    public string Name => "Pause service";

    public ActionInput ServiceToPause { get; set; }

    public PauseService()
    {
        _cSharpService = new CSharpService();
        _windowsServiceService = new WindowsServiceService();

        ServiceToPause = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var serviceToPauseValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ServiceToPause);
        _windowsServiceService.Pause(serviceToPauseValue);
    }
}