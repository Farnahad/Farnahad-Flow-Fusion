using System.Text;
using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Text;

public class CreateRandomText : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Create random text";

    public bool UserUppercaseLetters { get; set; }
    public bool UserLowercaseLetters { get; set; }
    public bool UseDigits { get; set; }
    public bool UseSymbols { get; set; }
    public ActionInput MinimumLength { get; set; }
    public ActionInput MaximumLength { get; set; }
    public Variable RandomText { get; set; }

    private readonly Random _random;

    public CreateRandomText()
    {
        _cSharpService = new CSharpService();
        _random = Random.Shared;

        UserUppercaseLetters = true;
        UserLowercaseLetters = true;
        UseDigits = true;
        UseSymbols = true;
        MinimumLength = new ActionInput();
        MaximumLength = new ActionInput();
        RandomText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var minimumLengthValue = await _cSharpService.EvaluateActionInput<int>(sandBox, MinimumLength);
        var maximumLengthValue = await _cSharpService.EvaluateActionInput<int>(sandBox, MaximumLength);

        var stringBuilder = new StringBuilder();

        if (UserUppercaseLetters)
        {

            // ReSharper disable once StringLiteralTypo
            stringBuilder.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }
        if (UserLowercaseLetters)
        {
            // ReSharper disable once StringLiteralTypo
            stringBuilder.Append("abcdefghijklmnopqrstuvwxyz");
        }

        if (UseDigits)
            stringBuilder.Append("0123456789");

        if (UseSymbols)
            stringBuilder.Append("!@#$%^&*()_+-=[]{}|;:,.<>?/");

        if (stringBuilder.Length == 0)
            throw new InvalidOperationException("No character types selected");

        var length = _random.Next(minimumLengthValue, minimumLengthValue + 1);
        RandomText.Value = new string(Enumerable.Repeat(stringBuilder.ToString(), length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());

        sandBox.Variables.Add(RandomText);
    }
}