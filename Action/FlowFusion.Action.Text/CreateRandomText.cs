using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;

namespace FlowFusion.Action.Text;

public class CreateRandomText(ITextService textService) : IAction
{
    public string Name => "Create random text";

    public bool UserUppercaseLetters { get; set; } = true;
    public bool UserLowercaseLetters { get; set; } = true;
    public bool UseDigits { get; set; } = true;
    public bool UseSymbols { get; set; } = true;
    public ActionInput MinimumLength { get; set; } = new();
    public ActionInput MaximumLength { get; set; } = new();
    public Variable RandomText { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var minimumLengthValue = await sandBox.EvaluateActionInput<int>(MinimumLength);
        var maximumLengthValue = await sandBox.EvaluateActionInput<int>(MaximumLength);

        RandomText.Value = textService.CreateRandomText(UserUppercaseLetters, UserLowercaseLetters,
            UseDigits, UseSymbols, minimumLengthValue, maximumLengthValue);

        sandBox.SetVariable(RandomText);
    }
}