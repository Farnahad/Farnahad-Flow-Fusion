using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;

namespace FlowFusion.Action.Text;

public class ConvertTextToDatetime(ITextService textService) : IAction
{
    public string Name => "Convert text to datetime";

    public ActionInput TextToConvert { get; set; } = new();
    public bool DateIsRepresentedInCustomFormat { get; set; } = false;
    public Variable TextAsDateTime { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToConvertValue = await sandBox.EvaluateActionInput<string>(TextToConvert);

        TextAsDateTime.Value = textService.ConvertTextToDatetime(textToConvertValue, DateIsRepresentedInCustomFormat);

        sandBox.SetVariable(TextAsDateTime);
    }
}