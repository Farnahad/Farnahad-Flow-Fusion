using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.CmdSession.CmdSession;

namespace FlowFusion.Action.CmdSession;

public class OpenCmdSession(ICmdSessionService cmdSessionService) : IAction
{
    public string Name => "Open CMD session";

    public ActionInput WorkingDirectory { get; set; } = new();
    public bool ChangeCodePage { get; set; } = false;
    public Variable CmdSession { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var workingDirectoryValue = await sandBox.EvaluateActionInput<string>(WorkingDirectory);

        CmdSession.Value = await cmdSessionService.OpenCmdSession(workingDirectoryValue, ChangeCodePage);

        sandBox.SetVariable(CmdSession);
    }
}