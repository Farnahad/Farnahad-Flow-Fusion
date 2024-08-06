using System.Text.RegularExpressions;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Text;

public class ParseText : IAction //XXXXXXXXXXXX
{
    public string Name => "Parse text";

    public ActionInput TextToParse { get; set; }
    public ActionInput TextToFind { get; set; }
    public bool IsRegularExpression { get; set; }
    public ActionInput StartParsingAtPosition { get; set; }
    public bool FirstOccurrenceOnly { get; set; }
    public bool IgnoreCase { get; set; }
    public Variable Position { get; set; }

    public ParseText()
    {
        TextToParse = new ActionInput();
        TextToFind = new ActionInput();
        IsRegularExpression = false;
        StartParsingAtPosition = new ActionInput();
        FirstOccurrenceOnly = true;
        IgnoreCase = false;
        Position = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToParseValue = await sandBox.EvaluateActionInput<string>(TextToParse);
        var textToFindValue = await sandBox.EvaluateActionInput<string>(TextToFind);
        var startParsingAtPositionValue = await sandBox.EvaluateActionInput<int>(StartParsingAtPosition);


        var comparisonType = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

        if (IsRegularExpression)
        {
            var regexOptions = IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;
            var regex = new Regex(textToFindValue, regexOptions);
            var match = regex.Match(textToParseValue, startParsingAtPositionValue);

            if (match.Success)
                Position.Value = match.Index;
        }
        else
        {
            var index = textToParseValue.IndexOf(textToFindValue, startParsingAtPositionValue, comparisonType);
            Position.Value = index;
        }

        sandBox.SetVariable(Position);
    }
}