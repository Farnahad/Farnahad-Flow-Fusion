using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Conditionals.Conditionals;
using FlowFusion.Service.Conditionals.Conditionals.Base.CaseBase;

namespace FlowFusion.Action.Conditionals;

public class ElseIf(IConditionalsService conditionalsService) : IAction
{
    public string Name => "Else If";

    public ActionInput FirstOperand { get; set; } = new();
    public Operator Operator { get; set; } = Operator.EqualTo;
    public ActionInput SecondOperand { get; set; } = new();
    public bool IgnoreCase { get; set; } = false;
    public List<IAction> Actions { get; set; } = new();
    public bool ElseIfResult { get; set; }

    public async Task Execute(SandBox sandBox)
    {
        var firstOperand = await sandBox.EvaluateActionInput<object>(FirstOperand);
        var secondOperand = await sandBox.EvaluateActionInput<object>(SecondOperand);
        var stringComparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase :
            StringComparison.CurrentCulture;

        ElseIfResult = conditionalsService.If(firstOperand, Operator, secondOperand, IgnoreCase);

        if (ElseIfResult)
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);
        }
    }
}