﻿using System.Globalization;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Text.ChangeTextCaseBase;

namespace FarnahadFlowFusion.Action.Text;

public class ChangeTextCase : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Change text case";

    public ActionInput TextToConvert { get; set; }
    public ConvertTo ConvertTo { get; set; }
    public Variable TextWithNewCase { get; set; }

    public ChangeTextCase()
    {
        _cSharpService = new CSharpService();

        TextToConvert = new ActionInput();
        ConvertTo = ConvertTo.UpperCase;
        TextWithNewCase = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToConvertValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToConvert);

        TextWithNewCase.Value = ConvertTo switch
        {
            ConvertTo.LowerCase => textToConvertValue.ToLower(),
            ConvertTo.SentenceCase => ToSentenceCase(textToConvertValue),
            ConvertTo.TitleCase => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textToConvertValue.ToLower()),
            ConvertTo.UpperCase => textToConvertValue.ToUpper(),
            _ => TextWithNewCase.Value
        };

        sandBox.Variables.Add(TextWithNewCase);
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