using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.System.System;
using FlowFusion.Service.System.System.Base;

namespace FlowFusion.Action.System;

public class WaitForProcess(ISystemService systemService) : IAction
{
    public string Name => "Wait for process";

    public ActionInput ProcessName { get; set; } = new();
    public WaitForProcessTo WaitForProcessTo { get; set; } = WaitForProcessTo.Start;
    public bool FailWithTimeoutError { get; set; } = false;
    public ActionInput Duration { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var processNameValue = await sandBox.EvaluateActionInput<string>(ProcessName);
        var durationValue = await sandBox.EvaluateActionInput<int>(Duration);

        await systemService.WaitForProcess(processNameValue, WaitForProcessTo, FailWithTimeoutError, durationValue);
    }
}