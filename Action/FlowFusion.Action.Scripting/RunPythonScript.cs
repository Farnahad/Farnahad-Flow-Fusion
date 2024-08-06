using System.Diagnostics;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Action.Scripting.RunPythonScriptBase;

namespace FlowFusion.Action.Scripting;

public class RunPythonScript : IAction
{
    public string Name => "Run Python script";

    public ActionInput PythonScriptToRun { get; set; }
    public PythonVersion PythonVersion { get; set; }
    public ActionInput ModuleFolderPaths { get; set; }
    public Variable PythonScriptOutput { get; set; }

    public RunPythonScript()
    {

        PythonScriptToRun = new ActionInput();
        PythonVersion = PythonVersion.Python27;
        ModuleFolderPaths = new ActionInput();
        PythonScriptOutput = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var pythonScriptToRunValue = await sandBox.EvaluateActionInput<string>(PythonScriptToRun);
        var moduleFolderPathsValue = await sandBox.EvaluateActionInput<string>(ModuleFolderPaths);

        var pythonPath = PythonVersion == PythonVersion.Python34 ? "python" : "python2";

        // Prepare process start info
        var processStartInfo = new ProcessStartInfo
        {
            FileName = pythonPath,
            Arguments = $"\"{pythonScriptToRunValue}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            // ReSharper disable once AssignNullToNotNullAttribute
            WorkingDirectory = Path.GetDirectoryName(pythonScriptToRunValue),
        };

        if (string.IsNullOrEmpty(moduleFolderPathsValue) == false)
        {
            var paths = moduleFolderPathsValue.Split(';');
            var pathEnv = string.Join(";", paths);
            processStartInfo.EnvironmentVariables["PYTHONPATH"] = pathEnv;
        }

        using (var process = new Process())
        {
            process.StartInfo = processStartInfo;
            process.Start();

            var outputTask = process.StandardOutput.ReadToEndAsync();
            var errorTask = process.StandardError.ReadToEndAsync();

            if (await Task.WhenAny(outputTask, errorTask) == outputTask)
            {
                PythonScriptOutput.Value = await outputTask;
            }
            else
            {
                process.Kill();
            }

            var errorOutput = await errorTask;
            if (string.IsNullOrEmpty(errorOutput) == false)
            {
            }
        }

        sandBox.SetVariable(PythonScriptOutput);
    }
}