using System.Text.RegularExpressions;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Text;

public class EscapeTextForRegularExpression : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Escape text for regular expression";

    public ActionInput TextToEscape { get; set; }
    public Variable EscapedText { get; set; }

    public EscapeTextForRegularExpression()
    {
        _cSharpService = new CSharpService();

        TextToEscape = new ActionInput();
        EscapedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToEscapeValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToEscape);

        EscapedText.Value = Regex.Escape(textToEscapeValue);

        sandBox.Variables.Add(EscapedText);
    }
}