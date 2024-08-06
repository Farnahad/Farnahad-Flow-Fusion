using System.Text.RegularExpressions;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Text;

public class EscapeTextForRegularExpression : IAction //XXXXXXXXXXXX
{
    public string Name => "Escape text for regular expression";

    public ActionInput TextToEscape { get; set; }
    public Variable EscapedText { get; set; }

    public EscapeTextForRegularExpression()
    {
        TextToEscape = new ActionInput();
        EscapedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToEscapeValue = await sandBox.EvaluateActionInput<string>(TextToEscape);

        EscapedText.Value = Regex.Escape(textToEscapeValue);

        sandBox.SetVariable(EscapedText);
    }
}