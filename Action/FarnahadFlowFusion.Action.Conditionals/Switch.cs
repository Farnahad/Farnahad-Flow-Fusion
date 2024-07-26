using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Conditionals;

public class Switch : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Switch";

    public ActionInput ValueToCheck { get; set; }
    public List<Case> Cases { get; set; }
    public DefaultCase DefaultCase { get; set; }

    public Switch()
    {
        _cSharpService = new CSharpService();

        ValueToCheck = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var valueToCheck = await _cSharpService.EvaluateActionInput<object>(sandBox, ValueToCheck);

        var caseResult = false;

        foreach (var @case in Cases)
        {
            @case.ValueToCheck = valueToCheck;
            await @case.Execute(sandBox);
            caseResult = @case.CaseResult;

            if (caseResult)
                break;
        }

        if (caseResult == false)
            await DefaultCase.Execute(sandBox);
    }
}