using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;

namespace FlowFusion.Action.Text;

public class EscapeTextForRegularExpression(ITextService textService) : IAction
{
    public string Name => "Escape text for regular expression";

    public ActionInput TextToEscape { get; set; } = new();
    public Variable EscapedText { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToEscapeValue = await sandBox.EvaluateActionInput<string>(TextToEscape);

        EscapedText.Value = textService.EscapeTextForRegularExpression(textToEscapeValue);

        sandBox.SetVariable(EscapedText);
    }
}