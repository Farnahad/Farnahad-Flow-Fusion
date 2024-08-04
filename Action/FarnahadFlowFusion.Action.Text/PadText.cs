using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Action.Text.PadTextBase;

namespace FarnahadFlowFusion.Action.Text;

public class PadText : IAction
{
    public string Name => "Pad text";

    public ActionInput TextToPad { get; set; }
    public Pad Pad { get; set; }
    public ActionInput TextForPadding { get; set; }
    public ActionInput TotalLength { get; set; }
    public Variable PaddedText { get; set; }

    public PadText()
    {
        TextToPad = new ActionInput();
        Pad = Pad.Left;
        TextForPadding = new ActionInput();
        TotalLength = new ActionInput();
        PaddedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToPadValue = await sandBox.EvaluateActionInput<string>(TextToPad);
        var textForPaddingValue = await sandBox.EvaluateActionInput<char>(TextForPadding);
        var totalLengthValue = await sandBox.EvaluateActionInput<int>(TotalLength);

        switch (Pad)
        {
            case Pad.Left:
                PaddedText.Value = textToPadValue.PadLeft(totalLengthValue, textForPaddingValue);
                break;
            case Pad.Right:
                PaddedText.Value = textToPadValue.PadRight(totalLengthValue, textForPaddingValue);
                break;
        }

        sandBox.SetVariable(PaddedText);
    }
}