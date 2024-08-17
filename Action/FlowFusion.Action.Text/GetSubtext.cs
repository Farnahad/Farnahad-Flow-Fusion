using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;
using FlowFusion.Service.Text.Text.Base;

namespace FlowFusion.Action.Text;

public class GetSubtext(ITextService textService) : IAction
{
    public string Name => "Get subtext";

    public ActionInput OriginalText { get; set; } = new();
    public SubtextStartIndex StartIndex { get; set; } = SubtextStartIndex.CharacterPosition;
    public ActionInput CharacterPosition { get; set; } = new();
    public SubtextLength Length { get; set; } = SubtextLength.NumberOfChars;
    public ActionInput NumberOfChars { get; set; } = new();
    public Variable Subtext { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var originalTextValue = await sandBox.EvaluateActionInput<string>(OriginalText);
        var characterPositionValue = await sandBox.EvaluateActionInput<int>(CharacterPosition);
        var numberOfCharsValue = await sandBox.EvaluateActionInput<int>(NumberOfChars);

        Subtext.Value = textService.GetSubtext(originalTextValue, StartIndex,
            characterPositionValue, Length, numberOfCharsValue);

        sandBox.SetVariable(Subtext);
    }
}