using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Action.Text.GetSubtextBase;

namespace FlowFusion.Action.Text;

public class GetSubtext : IAction
{
    public string Name => "Get subtext";

    public ActionInput OriginalText { get; set; }
    public StartIndex StartIndex { get; set; }
    public ActionInput CharacterPosition { get; set; }
    public Length Length { get; set; }
    public ActionInput NumberOfChars { get; set; }
    public Variable Subtext { get; set; }

    public GetSubtext()
    {
        OriginalText = new ActionInput();
        StartIndex = StartIndex.CharacterPosition;
        CharacterPosition = new ActionInput();
        Length = Length.NumberOfChars;
        NumberOfChars = new ActionInput();
        Subtext = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var originalTextValue = await sandBox.EvaluateActionInput<string>(OriginalText);
        var characterPositionValue = await sandBox.EvaluateActionInput<int>(CharacterPosition);
        var numberOfCharsValue = await sandBox.EvaluateActionInput<int>(NumberOfChars);

        int startIndex = 0;

        switch (this.StartIndex)
        {
            case StartIndex.CharacterPosition:
                startIndex = characterPositionValue;
                break;
            case StartIndex.StartOfText:
                startIndex = 0;
                break;
        }

        int length = 0;

        switch (this.Length)
        {
            case Length.EndOfText:
                length = originalTextValue.Length - startIndex;
                break;
            case Length.NumberOfChars:
                length = numberOfCharsValue;
                break;
        }

        Subtext.Value = originalTextValue.Substring(startIndex, length);

        sandBox.SetVariable(Subtext);
    }
}