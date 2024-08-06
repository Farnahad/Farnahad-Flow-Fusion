using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.WindowsServices.WindowsService;

namespace FlowFusion.Action.WindowsServices;

public class ResumeService : IAction
{
    private readonly WindowsServiceService _windowsServiceService;

    public string Name => "Resume service";

    public ActionInput ServiceToResume { get; set; }

    public ResumeService()
    {
        _windowsServiceService = new WindowsServiceService();

        ServiceToResume = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var serviceToResume = await sandBox.EvaluateActionInput<string>(ServiceToResume);
        _windowsServiceService.Resume(serviceToResume);
    }
}