using System.Diagnostics;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.CmdSession;

public class CloseCmdSession : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Close CMD session";

    public ActionInput CmdSession { get; set; }

    public CloseCmdSession()
    {
        _cSharpService = new CSharpService();

        CmdSession = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var cmdSessionValue = await _cSharpService.EvaluateActionInput<Process>(sandBox, CmdSession);

        cmdSessionValue.Kill(true);
    }
}