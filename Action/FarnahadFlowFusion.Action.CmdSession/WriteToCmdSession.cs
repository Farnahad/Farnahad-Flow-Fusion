using System.Diagnostics;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.CmdSession;

public class WriteToCmdSession : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Write to CMD session";

    public ActionInput CmdSession { get; set; }
    public ActionInput Command { get; set; }
    public bool SendEnterAfterCommand { get; set; }

    public WriteToCmdSession()
    {
        _cSharpService = new CSharpService();

        CmdSession = new ActionInput();
        Command = new ActionInput();
        SendEnterAfterCommand = true;
    }

    public async Task Execute(SandBox sandBox)
    {
        var cmdSessionValue = await _cSharpService.EvaluateActionInput<Process>(sandBox, CmdSession);
        var commandValue = await _cSharpService.EvaluateActionInput<string>(sandBox, Command);

        await cmdSessionValue.StandardInput.WriteLineAsync(commandValue);

        if (SendEnterAfterCommand)
            await cmdSessionValue.StandardInput.WriteLineAsync();

        await cmdSessionValue.StandardInput.WriteLineAsync("exit");
        await cmdSessionValue.WaitForExitAsync();
    }
}