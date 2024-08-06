using System.Diagnostics;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.CmdSession;

public class WriteToCmdSession : IAction
{
    public string Name => "Write to CMD session";

    public ActionInput CmdSession { get; set; }
    public ActionInput Command { get; set; }
    public bool SendEnterAfterCommand { get; set; }

    public WriteToCmdSession()
    {
        CmdSession = new ActionInput();
        Command = new ActionInput();
        SendEnterAfterCommand = true;
    }

    public async Task Execute(SandBox sandBox)
    {
        var cmdSessionValue = await sandBox.EvaluateActionInput<Process>(CmdSession);
        var commandValue = await sandBox.EvaluateActionInput<string>(Command);

        await cmdSessionValue.StandardInput.WriteLineAsync(commandValue);

        if (SendEnterAfterCommand)
            await cmdSessionValue.StandardInput.WriteLineAsync();

        await cmdSessionValue.StandardInput.WriteLineAsync("exit");
        await cmdSessionValue.WaitForExitAsync();
    }
}