using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Conditionals.Conditionals;
using FlowFusion.Service.Conditionals.Conditionals.Base.CaseBase;

namespace FlowFusion.Action.Conditionals;

public class If(IConditionalsService conditionalsService) : IAction
{
    public string Name => "If";

    public ActionInput FirstOperand { get; set; } = new();
    public Operator Operator { get; set; } = Operator.EqualTo;
    public ActionInput SecondOperand { get; set; } = new();
    public bool IgnoreCase { get; set; } = false;
    public List<IAction> Actions { get; set; } = new();
    public List<ElseIf> ElseIfs { get; set; }
    public Else Else { get; set; }

    public async Task Execute(SandBox sandBox)
    {
        var firstOperandValue = await sandBox.EvaluateActionInput<object>(FirstOperand);
        var secondOperandValue = await sandBox.EvaluateActionInput<object>(SecondOperand);

        var ifResult = conditionalsService.If(firstOperandValue, Operator, secondOperandValue, IgnoreCase);

        if (ifResult)
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);
        }
        else
        {
            var elseIfResult = false;

            foreach (var elseIf in ElseIfs)
            {
                await elseIf.Execute(sandBox);

                if (elseIf.ElseIfResult)
                {
                    elseIfResult = true;
                    break;
                }
            }

            if (elseIfResult == false)
                await Else.Execute(sandBox);
        }
    }
}