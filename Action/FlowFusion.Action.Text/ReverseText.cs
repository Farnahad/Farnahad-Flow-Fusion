using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;

namespace FlowFusion.Action.Text;

public class ReverseText(ITextService textService) : IAction
{
    public string Name => "Reverse text";

    public ActionInput TextToReverse { get; set; } = new();
    public Variable ReversedText { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToReverseValue = await sandBox.EvaluateActionInput<string>(TextToReverse);

        ReversedText.Value = textService.ReverseText(textToReverseValue);

        sandBox.SetVariable(ReversedText);
    }
}