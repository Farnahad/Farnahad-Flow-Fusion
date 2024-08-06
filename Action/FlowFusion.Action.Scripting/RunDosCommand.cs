using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Scripting;

public class RunDosCommand : IAction
{
    public string Name => "Run DOS command";

    public ActionInput DosCommandOrApplication { get; set; }
    public ActionInput WorkingFolder { get; set; }
    public bool FailAfterTimeout { get; set; }
    public bool ChangeCodePage { get; set; }
    public Variable CommandOutput { get; set; }
    public Variable CommandErrorOutput { get; set; }
    public Variable CommandExitCoded { get; set; }

    public RunDosCommand()
    {
        DosCommandOrApplication = new ActionInput();
        WorkingFolder = new ActionInput();
        FailAfterTimeout = false;
        ChangeCodePage = false;
        CommandOutput = new Variable();
        CommandErrorOutput = new Variable();
        CommandExitCoded = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var minimumVaXXXXXXXXXXXXlue = await sandBox.EvaluateActionInput<int>(XXXXXXXXXXXX);
        var XXXXXXXXXXXX = await sandBox.EvaluateActionInput<int>(XXXXXXXXXXXX);

        XXXXXXXXXXXX.Value = new Random().Next(XXXXXXXXXXXX, XXXXXXXXXXXX);

        sandBox.SetVariable(XXXXXXXXXXXX);
    }
}