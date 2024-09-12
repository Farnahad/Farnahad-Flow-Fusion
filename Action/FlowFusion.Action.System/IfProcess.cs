using System.ServiceProcess;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.System.System;

namespace FlowFusion.Action.System;

public class IfProcess(ISystemService systemService) : IAction
{
    public string Name => "If process";

    // ReSharper disable once InconsistentNaming
    public Service.System.System.Base.IfProcess _IfProcess { get; set; } = Service.System.System.Base.IfProcess.IsRunning;
    public ActionInput ProcessName { get; set; } = new();

    // ReSharper disable once CollectionNeverUpdated.Global
    public List<IAction> Actions { get; set; } = new();

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