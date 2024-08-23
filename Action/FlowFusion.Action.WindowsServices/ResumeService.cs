using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.WindowsServices.WindowsService;

namespace FlowFusion.Action.WindowsServices;

public class ResumeService(IWindowsServiceService windowsServiceService) : IAction
{
    public string Name => "Resume service";

    public ActionInput ServiceToResume { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var serviceToResume = await sandBox.EvaluateActionInput<string>(ServiceToResume);

        windowsServiceService.ResumeService(serviceToResume);
    }
}