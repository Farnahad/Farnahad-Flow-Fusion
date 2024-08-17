using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;

namespace FlowFusion.Action.Text;

public class ParseText(ITextService textService) : IAction
{
    public string Name => "Parse text";

    public ActionInput TextToParse { get; set; } = new();
    public ActionInput TextToFind { get; set; } = new();
    public bool IsRegularExpression { get; set; } = false;
    public ActionInput StartParsingAtPosition { get; set; } = new();
    public bool FirstOccurrenceOnly { get; set; } = true;
    public bool IgnoreCase { get; set; } = false;
    public Variable Position { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToParseValue = await sandBox.EvaluateActionInput<string>(TextToParse);
        var textToFindValue = await sandBox.EvaluateActionInput<string>(TextToFind);
        var startParsingAtPositionValue = await sandBox.EvaluateActionInput<int>(StartParsingAtPosition);

        Position.Value = textService.ParseText(textToParseValue, textToFindValue, IsRegularExpression,
            startParsingAtPositionValue, FirstOccurrenceOnly, IgnoreCase);

        sandBox.SetVariable(Position);
    }
}