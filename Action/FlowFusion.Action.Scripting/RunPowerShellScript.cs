using System.Diagnostics;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Scripting;

public class RunPowerShellScript : IAction //XXXXXXXXXXXX
{
    public string Name => "Run PowerShell script";

    public ActionInput PowerShellCodeToRun { get; set; }
    public bool FailAfterTimeout { get; set; }
    public ActionInput Timeout { get; set; }
    public Variable PowerShellOutput { get; set; }

    public RunPowerShellScript()
    {
        PowerShellCodeToRun = new ActionInput();
        FailAfterTimeout = false;
        PowerShellOutput = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var powerShellCodeToRunValue = await sandBox.EvaluateActionInput<string>(PowerShellCodeToRun);
        var timeoutValue = await sandBox.EvaluateActionInput<int>(Timeout);

        using (Process powerShellProcess = new Process())
        {
            powerShellProcess.StartInfo.FileName = "powershell.exe";
            powerShellProcess.StartInfo.Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{powerShellCodeToRunValue}\"";
            powerShellProcess.StartInfo.RedirectStandardOutput = true;
            powerShellProcess.StartInfo.RedirectStandardError = true;
            powerShellProcess.StartInfo.UseShellExecute = false;
            powerShellProcess.StartInfo.CreateNoWindow = true;

            powerShellProcess.Start();

            var outputTask = powerShellProcess.StandardOutput.ReadToEndAsync();
            var errorTask = powerShellProcess.StandardError.ReadToEndAsync();

            if (await Task.WhenAny(Task.WhenAll(outputTask, errorTask), Task.Delay(TimeSpan.FromSeconds(timeoutValue))) == outputTask)
            {
                await powerShellProcess.WaitForExitAsync();
                PowerShellOutput.Value = outputTask.Result + "\n" + errorTask.Result;
            }
            else
            {
                if (FailAfterTimeout == false)
                {
                    powerShellProcess.Kill();
                    PowerShellOutput.Value = "Script execution was aborted due to timeout.";
                }
            }
        }

        sandBox.SetVariable(PowerShellOutput);
    }
}