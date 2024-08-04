using System.Diagnostics;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.CmdSession;

public class OpenCmdSession : IAction
{
    public string Name => "Open CMD session";

    public ActionInput WorkingDirectory { get; set; }
    public bool ChangeCodePage { get; set; }
    public Variable CmdSession { get; set; }

    public OpenCmdSession()
    {
        WorkingDirectory = new ActionInput();
        ChangeCodePage = false;
        CmdSession = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var workingDirectoryValue = await sandBox.EvaluateActionInput<string>(WorkingDirectory);

        var startInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            WorkingDirectory = workingDirectoryValue,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = false
        };

        var process = Process.Start(startInfo);
        if (ChangeCodePage)
        {
            // ReSharper disable once PossibleNullReferenceException
            await process.StandardInput.WriteLineAsync("chcp 65001"); // Change code page to UTF-8
        }

        // ReSharper disable once PossibleNullReferenceException
        process.OutputDataReceived += (sender, args) =>
        {
            if (string.IsNullOrEmpty(args.Data) == false)
            {
            }
        };

        process.BeginOutputReadLine();
        await process.WaitForExitAsync();

        CmdSession.Value = process;

        sandBox.SetVariable(CmdSession);
    }
}