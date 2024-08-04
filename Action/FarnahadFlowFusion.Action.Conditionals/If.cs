using FarnahadFlowFusion.Action.Conditionals.IfBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.Conditionals;

public class If : IAction
{
    public string Name => "If";

    public ActionInput FirstOperand { get; set; }
    public Operator Operator { get; set; }
    public ActionInput SecondOperand { get; set; }
    public bool IgnoreCase { get; set; }
    public List<IAction> Actions { get; set; }
    public List<ElseIf> ElseIfs { get; set; }
    public Else Else { get; set; }

    public If()
    {
        FirstOperand = new ActionInput();
        Operator = Operator.EqualTo;
        SecondOperand = new ActionInput();
        IgnoreCase = false;
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        var ifResult = false;
        var firstOperandValue = await sandBox.EvaluateActionInput<object>(FirstOperand);
        var secondOperandValue = await sandBox.EvaluateActionInput<object>(SecondOperand);
        var stringComparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase :
            StringComparison.CurrentCulture;

        if (Operator == Operator.Contains)
        {
            ifResult = ((string)firstOperandValue).Contains((string)secondOperandValue, stringComparison);
        }
        else if (Operator == Operator.DoesNotContain)
        {
            ifResult = ((string)firstOperandValue).Contains((string)secondOperandValue, stringComparison) == false;
        }
        else if (Operator == Operator.DoesNotEndsWith)
        {
            ifResult = ((string)firstOperandValue).EndsWith((string)secondOperandValue, stringComparison) == false;
        }
        else if (Operator == Operator.DoesNotStartWith)
        {
            ifResult = ((string)firstOperandValue).StartsWith((string)secondOperandValue, stringComparison) == false;
        }
        else if (Operator == Operator.EndsWith)
        {
            ifResult = ((string)firstOperandValue).EndsWith((string)secondOperandValue, stringComparison);
        }
        else if (Operator == Operator.EqualTo)
        {
            ifResult = firstOperandValue == secondOperandValue;
        }
        else if (Operator == Operator.GraterThan)
        {
            ifResult = (double)firstOperandValue > (double)secondOperandValue;
        }
        else if (Operator == Operator.GraterThanOrEqualTo)
        {
            ifResult = (double)firstOperandValue >= (double)secondOperandValue;
        }
        else if (Operator == Operator.IsBlank)
        {
            ifResult = string.IsNullOrWhiteSpace((string)firstOperandValue);
        }
        else if (Operator == Operator.IsEmpty)
        {
            ifResult = ((string)firstOperandValue).Trim() == string.Empty;
        }
        else if (Operator == Operator.IsNotBlank)
        {
            ifResult = string.IsNullOrWhiteSpace((string)firstOperandValue) == false;
        }
        else if (Operator == Operator.IsNotEmpty)
        {
            ifResult = ((string)firstOperandValue).Trim() != string.Empty;
        }
        else if (Operator == Operator.LessThan)
        {
            ifResult = (double)firstOperandValue < (double)secondOperandValue;
        }
        else if (Operator == Operator.LessThanOrEqualTo)
        {
            ifResult = (double)firstOperandValue <= (double)secondOperandValue;
        }
        else if (Operator == Operator.NotEqualTo)
        {
            ifResult = firstOperandValue != secondOperandValue;
        }
        else if (Operator == Operator.StartsWith)
        {
            ifResult = ((string)firstOperandValue).StartsWith((string)secondOperandValue, stringComparison);
        }

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