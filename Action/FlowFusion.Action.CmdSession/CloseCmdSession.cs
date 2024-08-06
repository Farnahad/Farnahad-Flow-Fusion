using System.Diagnostics;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.CmdSession;

public class CloseCmdSession : IAction //XXXXXXXXXXXX
{
    public string Name => "Close CMD session";

    public ActionInput CmdSession { get; set; }

    public CloseCmdSession()
    {
        CmdSession = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var cmdSessionValue = await sandBox.EvaluateActionInput<Process>(CmdSession);

        cmdSessionValue.Kill(true);
    }
}