using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Text.GetSubtextBase;

namespace FarnahadFlowFusion.Action.Text;

public class GetSubtext : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Get subtext";

    public ActionInput OriginalText { get; set; }
    public StartIndex StartIndex { get; set; }
    public ActionInput CharacterPosition { get; set; }
    public Length Length { get; set; }
    public ActionInput NumberOfChars { get; set; }
    public Variable Subtext { get; set; }

    public GetSubtext()
    {
        _cSharpService = new CSharpService();

        OriginalText = new ActionInput();
        StartIndex = StartIndex.CharacterPosition;
        CharacterPosition = new ActionInput();
        Length = Length.NumberOfChars;
        NumberOfChars = new ActionInput();
        Subtext = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var originalTextValue = await _cSharpService.EvaluateActionInput<string>(sandBox, OriginalText);
        var characterPositionValue = await _cSharpService.EvaluateActionInput<int>(sandBox, CharacterPosition);
        var numberOfCharsValue = await _cSharpService.EvaluateActionInput<int>(sandBox, NumberOfChars);

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

        sandBox.Variables.Add(Subtext);
    }
}