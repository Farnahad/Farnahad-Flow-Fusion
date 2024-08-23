using System.Diagnostics;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.CmdSession.CmdSession;

namespace FlowFusion.Action.CmdSession;

public class WriteToCmdSession(ICmdSessionService cmdSessionService) : IAction
{
    public string Name => "Write to CMD session";

    public ActionInput CmdSession { get; set; } = new();
    public ActionInput Command { get; set; } = new();
    public bool SendEnterAfterCommand { get; set; } = true;

    public async Task Execute(SandBox sandBox)
    {
        var cmdSessionValue = await sandBox.EvaluateActionInput<Process>(CmdSession);
        var commandValue = await sandBox.EvaluateActionInput<string>(Command);

        await cmdSessionService.WriteToCmdSession(cmdSessionValue, commandValue, SendEnterAfterCommand);
    }
}