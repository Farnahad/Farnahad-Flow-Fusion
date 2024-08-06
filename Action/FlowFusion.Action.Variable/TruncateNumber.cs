using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Variable.TruncateNumberBase;

namespace FlowFusion.Action.Variable;

public class TruncateNumber : IAction //XXXXXXXXXXXX
{
    public string Name => "Truncate Number";

    public ActionInput NumberToTruncate { get; set; }
    public Operation Operation { get; set; }
    public Main.Variable.Variable TruncatedValue { get; set; }

    public TruncateNumber()
    {
        NumberToTruncate = new ActionInput();
        Operation = Operation.GetIntegerPart;
        TruncatedValue = new Main.Variable.Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var numberToTruncate = await sandBox.EvaluateActionInput<double>(NumberToTruncate);

        TruncatedValue.Value = Operation switch
        {
            Operation.GetIntegerPart => (int)Math.Floor(numberToTruncate),
            Operation.GetDecimalPart => numberToTruncate % 1,
            _ => TruncatedValue.Value
        };

        sandBox.SetVariable(TruncatedValue);
    }
}