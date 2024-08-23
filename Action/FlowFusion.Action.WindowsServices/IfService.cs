using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.WindowsServices.WindowsService;

namespace FlowFusion.Action.WindowsServices;

public class IfService(IWindowsServiceService windowsServiceService) : IAction
{
    public string Name => "If service";

    public Service.WindowsServices.WindowsService.Base.IfService _IfService { get; set; } =
        Service.WindowsServices.WindowsService.Base.IfService.IsRunning;
    public ActionInput ServiceName { get; set; } = new();
    public List<IAction> Actions { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var serviceNameValue = await sandBox.EvaluateActionInput<string>(ServiceName);

        if (windowsServiceService.IfService(_IfService, serviceNameValue))
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);
        }
    }
}