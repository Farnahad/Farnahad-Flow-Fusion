using System.Diagnostics;
using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Scripting;

public class RunVbScript : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Run VBScript";

    public ActionInput VbScriptToRun { get; set; }
    public bool FailAfterTimeout { get; set; }
    public ActionInput Timeout { get; set; }
    public Variable VbScriptOutput { get; set; }

    public RunVbScript()
    {
        _cSharpService = new CSharpService();

        VbScriptToRun = new ActionInput();
        FailAfterTimeout = false;
        VbScriptOutput = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var vbScriptToRunValue = await _cSharpService.EvaluateActionInput<string>(sandBox, VbScriptToRun);
        var timeoutValue = await _cSharpService.EvaluateActionInput<int>(sandBox, Timeout);

        var processStartInfo = new ProcessStartInfo
        {
            FileName = "cscript.exe",
            Arguments = $"\"{vbScriptToRunValue}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        using (var process = new Process())
        {
            process.StartInfo = processStartInfo;
            process.Start();

            var outputTask = process.StandardOutput.ReadToEndAsync();
            var errorTask = process.StandardError.ReadToEndAsync();

            if (await Task.WhenAny(outputTask, Task.Delay(TimeSpan.FromSeconds(timeoutValue))) == outputTask)
                VbScriptOutput.Value = await outputTask;
            else
                process.Kill();

            var errorOutput = await errorTask;
            if (string.IsNullOrEmpty(errorOutput) == false)
            {
            }
        }

        sandBox.Variables.Add(VbScriptOutput);
    }
}