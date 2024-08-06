using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Conditionals;

public class Switch : IAction
{
    public string Name => "Switch";

    public ActionInput ValueToCheck { get; set; }
    public List<Case> Cases { get; set; }
    public DefaultCase DefaultCase { get; set; }

    public Switch()
    {
        ValueToCheck = new ActionInput();
    }

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