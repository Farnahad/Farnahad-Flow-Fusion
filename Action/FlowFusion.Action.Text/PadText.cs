using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;
using FlowFusion.Service.Text.Text.Base;

namespace FlowFusion.Action.Text;

public class PadText(ITextService textService) : IAction
{
    public string Name => "Pad text";

    public ActionInput TextToPad { get; set; } = new();
    public Pad Pad { get; set; } = Pad.Left;
    public ActionInput TextForPadding { get; set; } = new();
    public ActionInput TotalLength { get; set; } = new();
    public Variable PaddedText { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToPadValue = await sandBox.EvaluateActionInput<string>(TextToPad);
        var textForPaddingValue = await sandBox.EvaluateActionInput<char>(TextForPadding);
        var totalLengthValue = await sandBox.EvaluateActionInput<int>(TotalLength);

        PaddedText.Value = textService.PadText(textToPadValue, Pad, textForPaddingValue, totalLengthValue);

        sandBox.SetVariable(PaddedText);
    }
}