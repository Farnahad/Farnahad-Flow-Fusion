using FarnahadFlowFusion.Action.Conditionals.ElseIfBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Conditionals;

public class ElseIf : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Else If";

    public ActionInput FirstOperand { get; set; }
    public Operator Operator { get; set; }
    public ActionInput SecondOperand { get; set; }
    public bool IgnoreCase { get; set; }
    public List<IAction> Actions { get; set; }
    public bool ElseIfResult { get; set; }

    public ElseIf()
    {
        _cSharpService = new CSharpService();

        FirstOperand = new ActionInput();
        Operator = Operator.EqualTo;
        SecondOperand = new ActionInput();
        IgnoreCase = false;
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        ElseIfResult = false;

        var firstOperand = await _cSharpService.EvaluateActionInput<object>(sandBox, FirstOperand);
        var secondOperand = await _cSharpService.EvaluateActionInput<object>(sandBox, SecondOperand);
        var stringComparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase :
            StringComparison.CurrentCulture;

        if (Operator == Operator.Contains)
        {
            ElseIfResult = ((string)firstOperand).Contains((string)secondOperand, stringComparison);
        }
        else if (Operator == Operator.DoesNotContain)
        {
            ElseIfResult = ((string)firstOperand).Contains((string)secondOperand, stringComparison) == false;
        }
        else if (Operator == Operator.DoesNotEndsWith)
        {
            ElseIfResult = ((string)firstOperand).EndsWith((string)secondOperand, stringComparison) == false;
        }
        else if (Operator == Operator.DoesNotStartWith)
        {
            ElseIfResult = ((string)firstOperand).StartsWith((string)secondOperand, stringComparison) == false;
        }
        else if (Operator == Operator.EndsWith)
        {
            ElseIfResult = ((string)firstOperand).EndsWith((string)secondOperand, stringComparison);
        }
        else if (Operator == Operator.EqualTo)
        {
            ElseIfResult = firstOperand == secondOperand;
        }
        else if (Operator == Operator.GraterThan)
        {
            ElseIfResult = (double)firstOperand > (double)secondOperand;
        }
        else if (Operator == Operator.GraterThanOrEqualTo)
        {
            ElseIfResult = (double)firstOperand >= (double)secondOperand;
        }
        else if (Operator == Operator.IsBlank)
        {
            ElseIfResult = string.IsNullOrWhiteSpace((string)firstOperand);
        }
        else if (Operator == Operator.IsEmpty)
        {
            ElseIfResult = ((string)firstOperand).Trim() == string.Empty;
        }
        else if (Operator == Operator.IsNotBlank)
        {
            ElseIfResult = string.IsNullOrWhiteSpace((string)firstOperand) == false;
        }
        else if (Operator == Operator.IsNotEmpty)
        {
            ElseIfResult = ((string)firstOperand).Trim() != string.Empty;
        }
        else if (Operator == Operator.LessThan)
        {
            ElseIfResult = (double)firstOperand < (double)secondOperand;
        }
        else if (Operator == Operator.LessThanOrEqualTo)
        {
            ElseIfResult = (double)firstOperand <= (double)secondOperand;
        }
        else if (Operator == Operator.NotEqualTo)
        {
            ElseIfResult = firstOperand != secondOperand;
        }
        else if (Operator == Operator.StartsWith)
        {
            ElseIfResult = ((string)firstOperand).StartsWith((string)secondOperand, stringComparison);
        }

        if (ElseIfResult)
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);
        }
    }
}