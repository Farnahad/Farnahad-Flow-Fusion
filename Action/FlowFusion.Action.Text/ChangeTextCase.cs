using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;
using FlowFusion.Service.Text.Text.Base;

namespace FlowFusion.Action.Text;

public class ChangeTextCase(ITextService textService) : IAction
{
    public string Name => "Change text case";

    public ActionInput TextToConvert { get; set; } = new();
    public CaseConvertTo ConvertTo { get; set; } = CaseConvertTo.UpperCase;
    public Variable TextWithNewCase { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToConvertValue = await sandBox.EvaluateActionInput<string>(TextToConvert);

        TextWithNewCase.Value = textService.ChangeTextCase(textToConvertValue, ConvertTo);

        sandBox.SetVariable(TextWithNewCase);
    }
}