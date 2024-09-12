using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Conditionals.Conditionals;

namespace FlowFusion.Action.Conditionals;

public class Switch(IConditionalsService conditionalsService) : IAction
{
    private readonly IConditionalsService _conditionalsService = conditionalsService;
    public string Name => "Switch";

    public ActionInput ValueToCheck { get; set; } = new();
    public List<Case> Cases { get; set; }
    public DefaultCase DefaultCase { get; set; }

    public async Task Execute(SandBox sandBox)
    {
        var valueToCheck = await sandBox.EvaluateActionInput<object>(ValueToCheck);

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