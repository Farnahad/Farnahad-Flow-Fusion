using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.WindowsServices;

public class StartService : IAction
{
    private readonly CSharpService _cSharpService;
    private readonly WindowsServiceService _windowsServiceService;

    public string Name => "Start service";

    public ActionInput ServiceToStart { get; set; }

    public StartService()
    {
        _cSharpService = new CSharpService();
        _windowsServiceService = new WindowsServiceService();

        ServiceToStart = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var serviceToStart = await _cSharpService.EvaluateActionInput<string>(sandBox, ServiceToStart);
        _windowsServiceService.Start(serviceToStart);
    }
}