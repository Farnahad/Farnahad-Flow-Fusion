using System.Diagnostics;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.CmdSession;

public class ReadFromCmdSession : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Read from CMD session";

    public ActionInput CmdSession { get; set; }
    public bool SeparateOutputFromError { get; set; }
    public Variable CmdOutput { get; set; }

    public ReadFromCmdSession()
    {
        _cSharpService = new CSharpService();

        CmdSession = new ActionInput();
        SeparateOutputFromError = false;
        CmdOutput = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var cmdSessionValue = await _cSharpService.EvaluateActionInput<Process>(sandBox, CmdSession);

        if (SeparateOutputFromError)
        {
            cmdSessionValue.OutputDataReceived += (sender, args) =>
            {
                if (string.IsNullOrEmpty(args.Data) == false)
                    CmdOutput.Value = args.Data;
            };

            cmdSessionValue.ErrorDataReceived += (sender, args) =>
            {
                if (string.IsNullOrEmpty(args.Data) == false)
                    CmdOutput.Value = args.Data;
            };

            cmdSessionValue.BeginOutputReadLine();
            cmdSessionValue.BeginErrorReadLine();
        }
        else
        {
            cmdSessionValue.OutputDataReceived += (sender, args) =>
            {
                if (string.IsNullOrEmpty(args.Data) == false)
                    CmdOutput.Value = args.Data;
            };

            cmdSessionValue.BeginOutputReadLine();
        }

        await cmdSessionValue.WaitForExitAsync();

        sandBox.Variables.Add(CmdOutput);
    }
}