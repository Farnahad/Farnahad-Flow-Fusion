using System.Diagnostics;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.CmdSession.CmdSession;

namespace FlowFusion.Action.CmdSession;

public class CloseCmdSession(ICmdSessionService cmdSessionService) : IAction
{
    public string Name => "Close CMD session";

    public ActionInput CmdSession { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var cmdSessionValue = await sandBox.EvaluateActionInput<Process>(CmdSession);

        cmdSessionService.CloseCmdSession(cmdSessionValue);
    }
}