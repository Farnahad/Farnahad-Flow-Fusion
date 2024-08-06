using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Text;

public class ReverseText : IAction
{
    public string Name => "Reverse text";

    public ActionInput TextToReverse { get; set; }
    public Variable ReversedText { get; set; }

    public ReverseText()
    {
        TextToReverse = new ActionInput();
        ReversedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToReverseValue = await sandBox.EvaluateActionInput<string>(TextToReverse);

        ReversedText.Value = textToReverseValue.Reverse();

        sandBox.SetVariable(ReversedText);
    }
}