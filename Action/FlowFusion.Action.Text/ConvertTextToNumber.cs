using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;

namespace FlowFusion.Action.Text;

public class ConvertTextToNumber(ITextService textService) : IAction
{
    public string Name => "Convert text to number";

    public ActionInput TextToConvert { get; set; } = new();
    public Variable TextAsNumber { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToConvertValue = await sandBox.EvaluateActionInput<string>(TextToConvert);

        TextAsNumber.Value = textService.ConvertTextToNumber(textToConvertValue);

        sandBox.SetVariable(TextAsNumber);
    }
}