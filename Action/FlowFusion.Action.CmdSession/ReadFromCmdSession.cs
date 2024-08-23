using System.Diagnostics;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.CmdSession.CmdSession;

namespace FlowFusion.Action.CmdSession;

public class ReadFromCmdSession(ICmdSessionService cmdSessionService) : IAction
{
    public string Name => "Read from CMD session";

    public ActionInput CmdSession { get; set; } = new();
    public bool SeparateOutputFromError { get; set; } = false;
    public Variable CmdOutput { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var cmdSessionValue = await sandBox.EvaluateActionInput<Process>(CmdSession);

        CmdOutput.Value = await cmdSessionService.ReadFromCmdSession(cmdSessionValue, SeparateOutputFromError);

        sandBox.SetVariable(CmdOutput);
    }
}