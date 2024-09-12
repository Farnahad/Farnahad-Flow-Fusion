using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Variable.Variable;
using FlowFusion.Service.Variable.Variable.Base;

namespace FlowFusion.Action.Variable;

public class TruncateNumber(IVariableService variableService) : GeneralAction
{
    public override string Name => "Truncate Number";

    public ActionInput NumberToTruncate { get; set; } = new();
    public TruncateNumberOperation Operation { get; set; } = TruncateNumberOperation.GetIntegerPart;
    public Main.Variable.Variable TruncatedValue { get; set; } = new();

    public override async Task Execute(SandBox sandBox)
    {
        var numberToTruncateValue = await sandBox.EvaluateActionInput<double>(NumberToTruncate);

        TruncatedValue.Value = variableService.TruncateNumber(numberToTruncateValue, Operation);

        sandBox.SetVariable(TruncatedValue);
    }
}