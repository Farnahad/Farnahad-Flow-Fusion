using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Text;

public class ReverseText : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Reverse text";

    public ActionInput TextToReverse { get; set; }
    public Variable ReversedText { get; set; }

    public ReverseText()
    {
        _cSharpService = new CSharpService();

        TextToReverse = new ActionInput();
        ReversedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToReverseValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToReverse);

        ReversedText.Value = textToReverseValue.Reverse();

        sandBox.Variables.Add(ReversedText);
    }
}