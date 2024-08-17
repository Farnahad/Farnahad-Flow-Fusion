using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;

namespace FlowFusion.Action.Text;

public class ReplaceText(ITextService textService) : IAction
{
    public string Name => "Replace text";

    public ActionInput TextToParse { get; set; } = new();
    public ActionInput TextToFind { get; set; } = new();
    public bool UseRegularExpressionsForFindAndReplace { get; set; } = false;
    public bool IgnoreCase { get; set; } = false;
    public ActionInput ReplaceWith { get; set; } = new();
    public bool ActiveEscapeSequences { get; set; } = false;
    public Variable Replaced { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToParseValue = await sandBox.EvaluateActionInput<string>(TextToParse);
        var textToFindValue = await sandBox.EvaluateActionInput<string>(TextToFind);
        var replaceWithValue = await sandBox.EvaluateActionInput<string>(ReplaceWith);

        Replaced.Value = textService.ReplaceText(textToParseValue, textToFindValue,
            UseRegularExpressionsForFindAndReplace, IgnoreCase, replaceWithValue, ActiveEscapeSequences);

        sandBox.SetVariable(Replaced);
    }
}