using FlowFusion.Service.Conditionals.Conditionals.Base.CaseBase;

namespace FlowFusion.Service.Conditionals.Conditionals;

public class ConditionalsService : IConditionalsService
{
    public bool If(object firstOperand, Operator @operator, object secondOperand, bool ignoreCase)
    {
        var ifResult = false;
        var stringComparison = ignoreCase ? StringComparison.OrdinalIgnoreCase :
            StringComparison.CurrentCulture;

        if (@operator == Operator.Contains)
        {
            ifResult = ((string)firstOperand).Contains((string)secondOperand, stringComparison);
        }
        else if (@operator == Operator.DoesNotContain)
        {
            ifResult = ((string)firstOperand).Contains((string)secondOperand, stringComparison) == false;
        }
        else if (@operator == Operator.DoesNotEndsWith)
        {
            ifResult = ((string)firstOperand).EndsWith((string)secondOperand, stringComparison) == false;
        }
        else if (@operator == Operator.DoesNotStartWith)
        {
            ifResult = ((string)firstOperand).StartsWith((string)secondOperand, stringComparison) == false;
        }
        else if (@operator == Operator.EndsWith)
        {
            ifResult = ((string)firstOperand).EndsWith((string)secondOperand, stringComparison);
        }
        else if (@operator == Operator.EqualTo)
        {
            ifResult = firstOperand == secondOperand;
        }
        else if (@operator == Operator.GraterThan)
        {
            ifResult = (double)firstOperand > (double)secondOperand;
        }
        else if (@operator == Operator.GraterThanOrEqualTo)
        {
            ifResult = (double)firstOperand >= (double)secondOperand;
        }
        else if (@operator == Operator.IsBlank)
        {
            ifResult = string.IsNullOrWhiteSpace((string)firstOperand);
        }
        else if (@operator == Operator.IsEmpty)
        {
            ifResult = ((string)firstOperand).Trim() == string.Empty;
        }
        else if (@operator == Operator.IsNotBlank)
        {
            ifResult = string.IsNullOrWhiteSpace((string)firstOperand) == false;
        }
        else if (@operator == Operator.IsNotEmpty)
        {
            ifResult = ((string)firstOperand).Trim() != string.Empty;
        }
        else if (@operator == Operator.LessThan)
        {
            ifResult = (double)firstOperand < (double)secondOperand;
        }
        else if (@operator == Operator.LessThanOrEqualTo)
        {
            ifResult = (double)firstOperand <= (double)secondOperand;
        }
        else if (@operator == Operator.NotEqualTo)
        {
            ifResult = firstOperand != secondOperand;
        }
        else if (@operator == Operator.StartsWith)
        {
            ifResult = ((string)firstOperand).StartsWith((string)secondOperand, stringComparison);
        }

        return ifResult;
    }
}