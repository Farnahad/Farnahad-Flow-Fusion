using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Variable.TruncateNumberBase;

namespace FarnahadFlowFusion.Action.Variable;

public class TruncateNumber : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Truncate Number";

    public ActionInput NumberToTruncate { get; set; }
    public Operation Operation { get; set; }
    public Variable TruncatedValue { get; set; }

    public TruncateNumber()
    {
        _cSharpService = new CSharpService();

        NumberToTruncate = new ActionInput();
        Operation = Operation.GetIntegerPart;
        TruncatedValue = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var numberToTruncate = await _cSharpService.EvaluateActionInput<double>(sandBox, NumberToTruncate);

        TruncatedValue.Value = Operation switch
        {
            Operation.GetIntegerPart => (int)Math.Floor(numberToTruncate),
            Operation.GetDecimalPart => numberToTruncate % 1,
            _ => TruncatedValue.Value
        };

        sandBox.Variables.Add(TruncatedValue);
    }
}