using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Scripting;

public class RunDosCommand : IAction
{
    private readonly CSharpService _cSharpService;

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
        _cSharpService = new CSharpService();

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
        var minimumVaXXXXXXXXXXXXlue = await _cSharpService.EvaluateActionInput<int>(sandBox, XXXXXXXXXXXX);
        var XXXXXXXXXXXX = await _cSharpService.EvaluateActionInput<int>(sandBox, XXXXXXXXXXXX);

        XXXXXXXXXXXX.Value = new Random().Next(XXXXXXXXXXXX, XXXXXXXXXXXX);

        sandBox.Variables.Add(XXXXXXXXXXXX);
    }
}