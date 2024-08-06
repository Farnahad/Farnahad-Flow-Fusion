using System.ServiceProcess;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.WindowsServices.WindowsService;

namespace FlowFusion.Action.System;

public class IfProcess : IAction
{
    private readonly WindowsServiceService _windowsServiceService;

    public string Name => "If process";

    public IfProcessBase.IfProcess _IfProcess { get; set; }
    public ActionInput ProcessName { get; set; }
    public List<IAction> Actions { get; set; }

    public IfProcess()
    {
        _windowsServiceService = new WindowsServiceService();

        _IfProcess = IfProcessBase.IfProcess.IsRunning;
        ProcessName = new ActionInput();
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        var ifResult = false;
        var processNameValue = await sandBox.EvaluateActionInput<string>(ProcessName);

        switch (_IfProcess)
        {
            case IfProcessBase.IfProcess.IsNotRunning:
                ifResult = _windowsServiceService.GetStatus(processNameValue) != ServiceControllerStatus.Running;
                break;
            case IfProcessBase.IfProcess.IsRunning:
                ifResult = _windowsServiceService.GetStatus(processNameValue) == ServiceControllerStatus.Running;
                break;
        }

        if (ifResult)
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);
        }
    }
}