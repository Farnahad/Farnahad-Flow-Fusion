using System.Diagnostics;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.CmdSession;

public class ReadFromCmdSession : IAction
{
    public string Name => "Read from CMD session";

    public ActionInput CmdSession { get; set; }
    public bool SeparateOutputFromError { get; set; }
    public Variable CmdOutput { get; set; }

    public ReadFromCmdSession()
    {
        CmdSession = new ActionInput();
        SeparateOutputFromError = false;
        CmdOutput = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var cmdSessionValue = await sandBox.EvaluateActionInput<Process>(CmdSession);

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

        sandBox.SetVariable(CmdOutput);
    }
}