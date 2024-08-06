using System.Diagnostics;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.CmdSession;

public class ReadFromCmdSession : IAction //XXXXXXXXXXXX
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