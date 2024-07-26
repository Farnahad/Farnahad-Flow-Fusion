using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Text.PadTextBase;

namespace FarnahadFlowFusion.Action.Text;

public class PadText : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Pad text";

    public ActionInput TextToPad { get; set; }
    public Pad Pad { get; set; }
    public ActionInput TextForPadding { get; set; }
    public ActionInput TotalLength { get; set; }
    public Variable PaddedText { get; set; }

    public PadText()
    {
        _cSharpService = new CSharpService();

        TextToPad = new ActionInput();
        Pad = Pad.Left;
        TextForPadding = new ActionInput();
        TotalLength = new ActionInput();
        PaddedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToPadValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToPad);
        var textForPaddingValue = await _cSharpService.EvaluateActionInput<char>(sandBox, TextForPadding);
        var totalLengthValue = await _cSharpService.EvaluateActionInput<int>(sandBox, TotalLength);

        switch (Pad)
        {
            case Pad.Left:
                PaddedText.Value = textToPadValue.PadLeft(totalLengthValue, textForPaddingValue);
                break;
            case Pad.Right:
                PaddedText.Value = textToPadValue.PadRight(totalLengthValue, textForPaddingValue);
                break;
        }

        sandBox.Variables.Add(PaddedText);
    }
}