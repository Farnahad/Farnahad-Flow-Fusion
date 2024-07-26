using FarnahadFlowFusion.Action.Conditionals.CaseBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Conditionals;

public class Case : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Case";

    public Operator Operator { get; set; }
    public ActionInput ValueToCompare { get; set; }
    public bool IgnoreCase { get; set; }
    public List<IAction> Actions { get; set; }
    public object ValueToCheck { get; set; }
    public bool CaseResult { get; set; }

    public Case()
    {
        _cSharpService = new CSharpService();

        Operator = Operator.Contains;
        ValueToCompare = new ActionInput();
        IgnoreCase = false;
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        CaseResult = false;
        var valueToCompare = await _cSharpService.EvaluateActionInput<object>(sandBox, ValueToCompare);
        var stringComparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase :
            StringComparison.CurrentCulture;

        if (Operator == Operator.Contains)
        {
            CaseResult = ((string)valueToCompare).Contains((string)ValueToCheck, stringComparison);
        }
        else if (Operator == Operator.DoesNotContain)
        {
            CaseResult = ((string)valueToCompare).Contains((string)ValueToCheck, stringComparison) == false;
        }
        else if (Operator == Operator.DoesNotEndsWith)
        {
            CaseResult = ((string)valueToCompare).EndsWith((string)ValueToCheck, stringComparison) == false;
        }
        else if (Operator == Operator.DoesNotStartWith)
        {
            CaseResult = ((string)valueToCompare).StartsWith((string)ValueToCheck, stringComparison) == false;
        }
        else if (Operator == Operator.EndsWith)
        {
            CaseResult = ((string)valueToCompare).EndsWith((string)ValueToCheck, stringComparison);
        }
        else if (Operator == Operator.EqualTo)
        {
            CaseResult = valueToCompare == ValueToCheck;
        }
        else if (Operator == Operator.GraterThan)
        {
            CaseResult = (double)valueToCompare > (double)ValueToCheck;
        }
        else if (Operator == Operator.GraterThanOrEqualTo)
        {
            CaseResult = (double)valueToCompare >= (double)ValueToCheck;
        }
        else if (Operator == Operator.IsBlank)
        {
            CaseResult = string.IsNullOrWhiteSpace((string)valueToCompare);
        }
        else if (Operator == Operator.IsEmpty)
        {
            CaseResult = ((string)valueToCompare).Trim() == string.Empty;
        }
        else if (Operator == Operator.IsNotBlank)
        {
            CaseResult = string.IsNullOrWhiteSpace((string)valueToCompare) == false;
        }
        else if (Operator == Operator.IsNotEmpty)
        {
            CaseResult = ((string)valueToCompare).Trim() != string.Empty;
        }
        else if (Operator == Operator.LessThan)
        {
            CaseResult = (double)valueToCompare < (double)ValueToCheck;
        }
        else if (Operator == Operator.LessThanOrEqualTo)
        {
            CaseResult = (double)valueToCompare <= (double)ValueToCheck;
        }
        else if (Operator == Operator.NotEqualTo)
        {
            CaseResult = valueToCompare != ValueToCheck;
        }
        else if (Operator == Operator.StartsWith)
        {
            CaseResult = ((string)valueToCompare).StartsWith((string)ValueToCheck, stringComparison);
        }

        if (CaseResult)
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);
        }
    }
}