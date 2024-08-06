using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Action.Text.PadTextBase;

namespace FlowFusion.Action.Text;

public class PadText : IAction //XXXXXXXXXXXX
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