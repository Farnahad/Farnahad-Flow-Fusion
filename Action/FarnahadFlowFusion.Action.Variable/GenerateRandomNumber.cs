using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Variable;

public class GenerateRandomNumber : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Generate Random Number";

    public ActionInput MinimumValue { get; set; }
    public ActionInput MaximumValue { get; set; }
    public Variable RandomNumber { get; set; }

    public GenerateRandomNumber()
    {
        _cSharpService = new CSharpService();

        MinimumValue = new ActionInput();
        MaximumValue = new ActionInput();
        RandomNumber = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var minimumValue = await _cSharpService.EvaluateActionInput<int>(sandBox, MinimumValue);
        var maximumValue = await _cSharpService.EvaluateActionInput<int>(sandBox, MaximumValue);

        RandomNumber.Value = new Random().Next(minimumValue, maximumValue);

        sandBox.Variables.Add(RandomNumber);
    }
}