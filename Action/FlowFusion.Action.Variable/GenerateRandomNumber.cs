using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Variable;

public class GenerateRandomNumber : IAction //XXXXXXXXXXXX
{
    public string Name => "Generate Random Number";

    public ActionInput MinimumValue { get; set; }
    public ActionInput MaximumValue { get; set; }
    public Main.Variable.Variable RandomNumber { get; set; }

    public GenerateRandomNumber()
    {
        MinimumValue = new ActionInput();
        MaximumValue = new ActionInput();
        RandomNumber = new Main.Variable.Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var minimumValue = await sandBox.EvaluateActionInput<int>(MinimumValue);
        var maximumValue = await sandBox.EvaluateActionInput<int>(MaximumValue);

        RandomNumber.Value = new Random().Next(minimumValue, maximumValue);

        sandBox.SetVariable(RandomNumber);
    }
}