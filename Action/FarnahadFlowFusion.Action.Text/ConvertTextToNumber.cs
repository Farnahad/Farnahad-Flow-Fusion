using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Text;

public class ConvertTextToNumber : IAction
{
    private readonly CSharpService _cSharpService;
    private readonly VariableService _variableService;

    public string Name => "Convert text to number";

    public ActionInput TextToConvert { get; set; }
    public Variable TextAsNumber { get; set; }

    public ConvertTextToNumber()
    {
        _cSharpService = new CSharpService();
        _variableService = new VariableService();

        TextToConvert = new ActionInput();
        TextAsNumber = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToConvertValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToConvert);

        var variableType = _variableService.GetVariableType(textToConvertValue);
        TextAsNumber.Value = variableType switch
        {
            VariableType.Double => double.Parse(textToConvertValue),
            VariableType.Integer => int.Parse(textToConvertValue),
            _ => TextAsNumber.Value
        };

        sandBox.Variables.Add(TextAsNumber);
    }
}