using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Variable.Variable;

namespace FlowFusion.Action.Variable;

public class GenerateRandomNumber(IVariableService variableService) : GeneralAction
{
    public override string Name => "Generate Random Number";

    public ActionInput MinimumValue { get; set; } = new();
    public ActionInput MaximumValue { get; set; } = new();
    public Main.Variable.Variable RandomNumber { get; set; } = new();

    public override async Task Execute(SandBox sandBox)
    {
        var minimumValue = await sandBox.EvaluateActionInput<int>(MinimumValue);
        var maximumValue = await sandBox.EvaluateActionInput<int>(MaximumValue);

        RandomNumber.Value = variableService.GenerateRandomNumber(minimumValue, maximumValue);

        sandBox.SetVariable(RandomNumber);
    }
}