using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Conditionals.Conditionals;
using FlowFusion.Service.Conditionals.Conditionals.Base.CaseBase;

namespace FlowFusion.Action.Conditionals;

// ReSharper disable once ClassNeverInstantiated.Global
public class Case(IConditionalsService conditionalsService) : IAction
{
    public string Name => "Case";

    public Operator Operator { get; set; } = Operator.Contains;
    public ActionInput ValueToCompare { get; set; } = new();
    public bool IgnoreCase { get; set; } = false;
    public List<IAction> Actions { get; set; } = new();
    public object ValueToCheck { get; set; }
    public bool CaseResult { get; set; }

    public async Task Execute(SandBox sandBox)
    {
        var valueToCompare = await sandBox.EvaluateActionInput<object>(ValueToCompare);
        CaseResult = conditionalsService.If(valueToCompare, Operator, ValueToCheck, IgnoreCase);

        if (CaseResult)
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);
        }
    }
}