using System.Diagnostics;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.System.TerminateProcessBase;

namespace FlowFusion.Action.System;

public class TerminateProcess : IAction
{
    public string Name => "Terminate process";

    public SpecifyProcessBy SpecifyProcessBy { get; set; }
    public ActionInput ProcessName { get; set; }
    public ActionInput ProcessId { get; set; }

    public TerminateProcess()
    {
        SpecifyProcessBy = SpecifyProcessBy.ProcessName;
        ProcessName = new ActionInput();
        ProcessId = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var processNameValue = await sandBox.EvaluateActionInput<string>(ProcessName);
        var processIdValue = await sandBox.EvaluateActionInput<int>(ProcessId);

        switch (SpecifyProcessBy)
        {
            case SpecifyProcessBy.ProcessId:
                var processById = Process.GetProcessById(processIdValue);
                processById.Kill();
                await processById.WaitForExitAsync();
                break;
            case SpecifyProcessBy.ProcessName:
                var processes = Process.GetProcessesByName(processNameValue);
                foreach (var process in processes)
                {
                    process.Kill();
                    await process.WaitForExitAsync();
                }
                break;
        }
    }
}