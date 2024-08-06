using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Text;

public class ReverseText : IAction //XXXXXXXXXXXX
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