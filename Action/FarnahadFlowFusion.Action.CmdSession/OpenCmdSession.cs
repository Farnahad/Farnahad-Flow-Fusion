using System.Diagnostics;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.CmdSession;

public class OpenCmdSession : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Open CMD session";

    public ActionInput WorkingDirectory { get; set; }
    public bool ChangeCodePage { get; set; }
    public Variable CmdSession { get; set; }

    public OpenCmdSession()
    {
        _cSharpService = new CSharpService();

        WorkingDirectory = new ActionInput();
        ChangeCodePage = false;
        CmdSession = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var workingDirectoryValue = await _cSharpService.EvaluateActionInput<string>(sandBox, WorkingDirectory);

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

        sandBox.Variables.Add(CmdSession);
    }
}