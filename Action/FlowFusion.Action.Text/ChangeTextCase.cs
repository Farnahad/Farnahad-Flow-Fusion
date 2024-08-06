using System.Globalization;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Action.Text.ChangeTextCaseBase;

namespace FlowFusion.Action.Text;

public class ChangeTextCase : IAction //XXXXXXXXXXXX
{
    public string Name => "Change text case";

    public ActionInput TextToConvert { get; set; }
    public ConvertTo ConvertTo { get; set; }
    public Variable TextWithNewCase { get; set; }

    public ChangeTextCase()
    {
        TextToConvert = new ActionInput();
        ConvertTo = ConvertTo.UpperCase;
        TextWithNewCase = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToConvertValue = await sandBox.EvaluateActionInput<string>(TextToConvert);

        TextWithNewCase.Value = ConvertTo switch
        {
            ConvertTo.LowerCase => textToConvertValue.ToLower(),
            ConvertTo.SentenceCase => ToSentenceCase(textToConvertValue),
            ConvertTo.TitleCase => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textToConvertValue.ToLower()),
            ConvertTo.UpperCase => textToConvertValue.ToUpper(),
            _ => TextWithNewCase.Value
        };

        sandBox.SetVariable(TextWithNewCase);
    }

    private string ToSentenceCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var chars = input.ToCharArray();
        if (chars.Length >= 1)
        {
            if (char.IsLower(chars[0]))
                chars[0] = char.ToUpper(chars[0]);
        }

        for (var i = 1; i < chars.Length; i++)
        {
            if (chars[i - 1] == '.' || chars[i - 1] == '?' || chars[i - 1] == '!')
            {
                if (char.IsLower(chars[i]))
                    chars[i] = char.ToUpper(chars[i]);
            }
        }

        return new string(chars);
    }
}