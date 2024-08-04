using System.Text.RegularExpressions;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Text;

public class ParseText : IAction
{
    private readonly CSharpService _cSharpService;

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
        _cSharpService = new CSharpService();

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
        var textToParseValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToParse);
        var textToFindValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToFind);
        var startParsingAtPositionValue = await _cSharpService.EvaluateActionInput<int>(sandBox, StartParsingAtPosition);


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

        sandBox.Variables.Add(Position);
    }
}