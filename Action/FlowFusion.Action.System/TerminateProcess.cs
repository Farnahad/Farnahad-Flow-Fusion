using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.System.System;
using FlowFusion.Service.System.System.Base;

namespace FlowFusion.Action.System;

public class TerminateProcess(ISystemService systemService) : IAction
{
    public string Name => "Terminate process";

    public SpecifyProcessBy SpecifyProcessBy { get; set; } = SpecifyProcessBy.ProcessName;
    public ActionInput ProcessName { get; set; } = new();
    public ActionInput ProcessId { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var processNameValue = await sandBox.EvaluateActionInput<string>(ProcessName);
        var processIdValue = await sandBox.EvaluateActionInput<int>(ProcessId);

        await systemService.TerminateProcess(SpecifyProcessBy, processNameValue, processIdValue);
    }
}