using FarnahadFlowFusion.Action.Loops.LoopConditionBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Loops;

public class LoopCondition : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Loop condition";

    public ActionInput FirstOperand { get; set; }
    public Operator Operator { get; set; }
    public ActionInput SecondOperand { get; set; }
    public List<IAction> Actions { get; set; }

    public LoopCondition()
    {
        _cSharpService = new CSharpService();

        FirstOperand = new ActionInput();
        Operator = Operator.EqualTo;
        SecondOperand = new ActionInput();
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        var firstOperandValue = await _cSharpService.EvaluateActionInput<double>(sandBox, FirstOperand);
        var secondOperandValue = await _cSharpService.EvaluateActionInput<double>(sandBox, SecondOperand);

        var whileResult = GetWhileResult(firstOperandValue, secondOperandValue);

        while (whileResult)
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);

            whileResult = GetWhileResult(firstOperandValue, secondOperandValue);
        }
    }

    public bool GetWhileResult(double firstOperandValue, double secondOperandValue)
    {
        var whileResult = false;

        if (Operator == Operator.EqualTo)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            whileResult = firstOperandValue == secondOperandValue;
        }
        else if (Operator == Operator.GraterThan)
        {
            whileResult = firstOperandValue > secondOperandValue;
        }
        else if (Operator == Operator.GraterThanOrEqualTo)
        {
            whileResult = firstOperandValue >= secondOperandValue;
        }
        else if (Operator == Operator.LessThan)
        {
            whileResult = firstOperandValue < secondOperandValue;
        }
        else if (Operator == Operator.LessThanOrEqualTo)
        {
            whileResult = firstOperandValue <= secondOperandValue;
        }
        else if (Operator == Operator.NotEqualTo)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            whileResult = firstOperandValue != secondOperandValue;
        }

        return whileResult;
    }
}