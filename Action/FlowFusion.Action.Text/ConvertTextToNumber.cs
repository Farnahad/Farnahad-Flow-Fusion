using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Text;

public class ConvertTextToNumber : IAction
{
    private readonly VariableService _variableService;

    public string Name => "Convert text to number";

    public ActionInput TextToConvert { get; set; }
    public Variable TextAsNumber { get; set; }

    public ConvertTextToNumber()
    {
        _variableService = new VariableService();

        TextToConvert = new ActionInput();
        TextAsNumber = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToConvertValue = await sandBox.EvaluateActionInput<string>(TextToConvert);

        var variableType = _variableService.GetVariableType(textToConvertValue);
        TextAsNumber.Value = variableType switch
        {
            VariableType.Double => double.Parse(textToConvertValue),
            VariableType.Integer => int.Parse(textToConvertValue),
            _ => TextAsNumber.Value
        };

        sandBox.SetVariable(TextAsNumber);
    }
}