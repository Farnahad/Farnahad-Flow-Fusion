using System.Diagnostics;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.System.WaitForProcessBase;

namespace FarnahadFlowFusion.Action.System;

public class WaitForProcess : IAction
{
    public string Name => "Wait for process";

    public ActionInput ProcessName { get; set; }
    public WaitForProcessTo WaitForProcessTo { get; set; }
    public bool FailWithTimeoutError { get; set; }
    public ActionInput Duration { get; set; }

    public WaitForProcess()
    {
        ProcessName = new ActionInput();
        WaitForProcessTo = WaitForProcessTo.Start;
        FailWithTimeoutError = false;
        Duration = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var processNameValue = await sandBox.EvaluateActionInput<string>(ProcessName);
        var durationValue = await sandBox.EvaluateActionInput<int>(Duration);


        var endTime = global::System.DateTime.UtcNow.AddSeconds(durationValue);

        var cancellationToken = new CancellationToken();

        bool waiteResult = false;

        while (global::System.DateTime.UtcNow < endTime)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var processExists = ProcessExists(processNameValue);
            if ((WaitForProcessTo == WaitForProcessTo.Start && processExists) ||
                (WaitForProcessTo == WaitForProcessTo.Stop && !processExists))
            {
                waiteResult = true;
                break;
            }

            await Task.Delay(500, cancellationToken);
        }

        if (FailWithTimeoutError && waiteResult == false)
        {
            throw new TimeoutException($"Timeout waiting for process '{processNameValue}' to {WaitCondition}.");
        }
    }

    private bool ProcessExists(string processName)
    {
        var processes = Process.GetProcessesByName(processName);
        return processes.Length > 0;
    }
}