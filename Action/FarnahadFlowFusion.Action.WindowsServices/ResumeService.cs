using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.WindowsServices;

public class ResumeService : IAction
{
    private readonly CSharpService _cSharpService;
    private readonly WindowsServiceService _windowsServiceService;

    public string Name => "Resume service";

    public ActionInput ServiceToResume { get; set; }

    public ResumeService()
    {
        _cSharpService = new CSharpService();
        _windowsServiceService = new WindowsServiceService();

        ServiceToResume = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var serviceToResume = await _cSharpService.EvaluateActionInput<string>(sandBox, ServiceToResume);
        _windowsServiceService.Resume(serviceToResume);
    }
}