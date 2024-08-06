using System.ServiceProcess;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.WindowsServices.WindowsService;

namespace FlowFusion.Action.WindowsServices;

public class IfService : IAction //XXXXXXXXXXXX
{
    private readonly WindowsServiceService _windowsServiceService;

    public string Name => "If service";

    public IfServiceBase.IfService _IfService { get; set; }
    public ActionInput ServiceName { get; set; }
    public List<IAction> Actions { get; set; }

    public IfService()
    {
        _windowsServiceService = new WindowsServiceService();

        _IfService = IfServiceBase.IfService.IsRunning;
        ServiceName = new ActionInput();
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        var serviceNameValue = await sandBox.EvaluateActionInput<string>(ServiceName);

        var result = _IfService switch
        {
            IfServiceBase.IfService.IsInstalled => _windowsServiceService.IsInstalled(serviceNameValue),
            IfServiceBase.IfService.IsNotInstalled => _windowsServiceService.IsInstalled(serviceNameValue) == false,
            IfServiceBase.IfService.IsPaused => _windowsServiceService.GetStatus(serviceNameValue) == ServiceControllerStatus.Paused,
            IfServiceBase.IfService.IsRunning => _windowsServiceService.GetStatus(serviceNameValue) == ServiceControllerStatus.Running,
            IfServiceBase.IfService.IsStopped => _windowsServiceService.GetStatus(serviceNameValue) == ServiceControllerStatus.Stopped,
            _ => false
        };

        if (result)
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);
        }
    }
}