using System.Text.RegularExpressions;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Text;

public class ReplaceText : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Replace text";

    public ActionInput TextToParse { get; set; }
    public ActionInput TextToFind { get; set; }
    public bool UseRegularExpressionsForFindAndReplace { get; set; }
    public bool IgnoreCase { get; set; }
    public ActionInput ReplaceWith { get; set; }
    public bool ActiveEscapeSequences { get; set; }
    public Variable Replaced { get; set; }

    public ReplaceText()
    {
        _cSharpService = new CSharpService();

        TextToParse = new ActionInput();
        TextToFind = new ActionInput();
        UseRegularExpressionsForFindAndReplace = false;
        IgnoreCase = false;
        ReplaceWith = new ActionInput();
        ActiveEscapeSequences = false;
        Replaced = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToParseValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToParse);
        var textToFindValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToFind);
        var replaceWithValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ReplaceWith);


        if (ActiveEscapeSequences)
        {
            textToParseValue = EscapeSequences(textToParseValue);
            textToFindValue = EscapeSequences(textToFindValue);
            replaceWithValue = EscapeSequences(replaceWithValue);
        }

        if (UseRegularExpressionsForFindAndReplace)
        {
            var regexOptions = IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;
            Replaced.Value = Regex.Replace(textToParseValue, textToFindValue, replaceWithValue, regexOptions);
        }
        else
        {
            StringComparison comparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            Replaced.Value = textToParseValue.Replace(textToFindValue, replaceWithValue, comparison);
        }

        sandBox.Variables.Add(Replaced);
    }

    private string EscapeSequences(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        return text.Replace("\\n", "\n")
            .Replace("\\t", "\t")
            .Replace("\\r", "\r")
            .Replace("\\b", "\b")
            .Replace("\\f", "\f")
            .Replace("\\a", "\a")
            .Replace("\\v", "\v")
            .Replace("\\\"", "\"")
            .Replace("\\'", "'")
            .Replace("\\\\", "\\");
    }
}