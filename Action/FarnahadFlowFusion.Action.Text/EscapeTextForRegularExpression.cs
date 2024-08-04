using System.Text.RegularExpressions;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Text;

public class EscapeTextForRegularExpression : IAction
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