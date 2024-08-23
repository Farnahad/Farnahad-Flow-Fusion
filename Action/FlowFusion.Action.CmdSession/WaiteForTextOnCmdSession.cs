using System.Diagnostics;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.CmdSession.CmdSession;

namespace FlowFusion.Action.CmdSession;

public class WaiteForTextOnCmdSession(ICmdSessionService cmdSessionService) : IAction
{
    public string Name => "Waite for text on CMD session";

    public ActionInput CmdSession { get; set; } = new();
    public ActionInput TextToWait { get; set; } = new();
    public bool IsRegularExpression { get; set; } = false;
    public bool IgnoreCase { get; set; } = true;
    public ActionInput Timeout { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var cmdSessionValue = await sandBox.EvaluateActionInput<Process>(CmdSession);
        var textToWaitValue = await sandBox.EvaluateActionInput<string>(TextToWait);
        var timeoutValue = await sandBox.EvaluateActionInput<int>(Timeout);

        await cmdSessionService.WaiteForTextOnCmdSession(cmdSessionValue, textToWaitValue,
            IsRegularExpression, IgnoreCase, timeoutValue);
    }
}