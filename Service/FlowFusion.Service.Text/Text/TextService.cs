using System.Text.RegularExpressions;
using FlowFusion.Service.Text.Text.Base;
using System.Text;
using System.Globalization;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Main.Variable;

namespace FlowFusion.Service.Text.Text;

public class TextService(IVariableService variableService) : ITextService
{
    public string AppendLineToText(string originalText, string lineToAppend)
    {
        var stringBuilder = new StringBuilder(originalText);
        stringBuilder.AppendLine(lineToAppend);

        return stringBuilder.ToString();
    }

    public string ChangeTextCase(string textToConvert, CaseConvertTo convertTo)
    {
        return convertTo switch
        {
            CaseConvertTo.LowerCase => textToConvert.ToLower(),
            CaseConvertTo.SentenceCase => ToSentenceCase(textToConvert),
            CaseConvertTo.TitleCase => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textToConvert.ToLower()),
            CaseConvertTo.UpperCase => textToConvert.ToUpper(),
            _ => throw new ArgumentOutOfRangeException(nameof(convertTo), convertTo, null)
        };
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

    public string ConvertDatetimeToText(DateTime datetimeToConvert, DateTimeFormatToUse dateTimeFormatToUse,
        DateTimeStandardFormat dateTimeStandardFormat, string customFormat)
    {
        if (dateTimeFormatToUse == DateTimeFormatToUse.Custom)
        {
            return datetimeToConvert.ToString(customFormat);
        }
        else if (dateTimeFormatToUse == DateTimeFormatToUse.Standard)
        {
            return dateTimeStandardFormat switch
            {
                DateTimeStandardFormat.FullDatetimeLongTime => datetimeToConvert.ToString("F"),
                DateTimeStandardFormat.FullDatetimeShortTime => datetimeToConvert.ToString("f"),
                DateTimeStandardFormat.GeneralDatetimeLongTime => datetimeToConvert.ToString("G"),
                DateTimeStandardFormat.GeneralDatetimeShortTime => datetimeToConvert.ToString("g"),
                DateTimeStandardFormat.LongDate => datetimeToConvert.ToString("D"),
                DateTimeStandardFormat.LongTime => datetimeToConvert.ToString("T"),
                DateTimeStandardFormat.ShortDate => datetimeToConvert.ToString("d"),
                DateTimeStandardFormat.ShortTime => datetimeToConvert.ToString("t"),
                DateTimeStandardFormat.SortableDatetime => datetimeToConvert.ToString("s"),
                _ => throw new ArgumentOutOfRangeException(nameof(dateTimeStandardFormat), dateTimeStandardFormat, null)
            };
        }

        return null;
    }

    public string ConvertNumberToText(double numberToConvert, int decimalPlaces, bool userThousandsSeparator)
    {
        return userThousandsSeparator ? numberToConvert.ToString("N" + decimalPlaces)
            : numberToConvert.ToString("F" + decimalPlaces);
    }

    public DateTime ConvertTextToDatetime(string textToConvert, bool dateIsRepresentedInCustomFormat)
    {
        return DateTime.Parse(textToConvert);
    }

    public double ConvertTextToNumber(string textToConvert)
    {
        var variableType = variableService.GetVariableType(textToConvert);
        return variableType switch
        {
            VariableType.Number => double.Parse(textToConvert),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public string CreateHtmlContent(string html)
    {
        return html;
    }

    public string CreateRandomText(bool userUppercaseLetters, bool userLowercaseLetters, bool useDigits, bool useSymbols,
        int minimumLength, int maximumLength)
    {
        var random = Random.Shared;

        var stringBuilder = new StringBuilder();

        if (userUppercaseLetters)
        {

            // ReSharper disable once StringLiteralTypo
            stringBuilder.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }
        if (userLowercaseLetters)
        {
            // ReSharper disable once StringLiteralTypo
            stringBuilder.Append("abcdefghijklmnopqrstuvwxyz");
        }

        if (useDigits)
            stringBuilder.Append("0123456789");

        if (useSymbols)
            stringBuilder.Append("!@#$%^&*()_+-=[]{}|;:,.<>?/");

        if (stringBuilder.Length == 0)
            throw new InvalidOperationException("No character types selected");

        var length = random.Next(minimumLength, minimumLength + 1);
        return new string(Enumerable.Repeat(stringBuilder.ToString(), length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public (string, bool) CropText(string originalText, CropMode cropMode, string endFlag, bool ignoreCase)
    {
        var comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        int flagIndex = originalText.IndexOf(endFlag, comparison);

        var croppedText = originalText;
        var isFlagFound = false;

        if (flagIndex == -1)
        {
            croppedText = originalText;
            isFlagFound = false;
        }
        else
        {
            switch (cropMode)
            {
                case CropMode.GetTextAfterTheSpecifiedFlag:
                    croppedText = originalText.Substring(flagIndex + endFlag.Length);
                    isFlagFound = true;
                    break;
                case CropMode.GetTextBeforeTheSpecifiedFlag:
                    croppedText = originalText.Substring(0, flagIndex);
                    isFlagFound = true;
                    break;
                case CropMode.GetTextBetweenTheTwoSpecifiedFlags:
                    if (string.IsNullOrEmpty(endFlag) == false)
                    {
                        int endFlagIndex = originalText.IndexOf(endFlag, flagIndex + endFlag.Length, comparison);

                        if (endFlagIndex == -1)
                        {
                            croppedText = originalText;
                            isFlagFound = false;
                        }
                        else
                        {
                            croppedText = originalText.Substring(flagIndex + endFlag.Length,
                                endFlagIndex - (flagIndex + endFlag.Length));
                            isFlagFound = true;
                        }
                    }
                    break;
            }
        }

        return (croppedText, isFlagFound);
    }

    public string EscapeTextForRegularExpression(string textToEscape)
    {
        return Regex.Escape(textToEscape);
    }

    public string GetSubtext(string originalText, SubtextStartIndex subtextStartIndex, int characterPosition,
        SubtextLength subtextLength, int numberOfChars)
    {
        int startIndex = 0;

        switch (subtextStartIndex)
        {
            case SubtextStartIndex.CharacterPosition:
                startIndex = characterPosition;
                break;
            case SubtextStartIndex.StartOfText:
                startIndex = 0;
                break;
        }

        int length = 0;

        switch (subtextLength)
        {
            case SubtextLength.EndOfText:
                length = originalText.Length - startIndex;
                break;
            case SubtextLength.NumberOfChars:
                length = numberOfChars;
                break;
        }

        return originalText.Substring(startIndex, length);
    }

    public string JoinText(List<string> specifyTheListToJoin, JoinDelimiterToSeparateListItems joinDelimiterToSeparateListItems,
        StandardDelimiter standardDelimiter, string customDelimiter)
    {
        switch (joinDelimiterToSeparateListItems)
        {
            case JoinDelimiterToSeparateListItems.Custom:
                return string.Join(customDelimiter, specifyTheListToJoin);
                break;
            case JoinDelimiterToSeparateListItems.None:
                return string.Join("", specifyTheListToJoin);
                break;
            case JoinDelimiterToSeparateListItems.Standard:

                switch (standardDelimiter)
                {
                    case StandardDelimiter.NewLine:
                        return string.Join(Environment.NewLine, specifyTheListToJoin);
                        break;
                    case StandardDelimiter.Space:
                        return string.Join(" ", specifyTheListToJoin);
                        break;
                    case StandardDelimiter.Tab:
                        return string.Join("\t", specifyTheListToJoin);
                        break;
                }
                break;
        }

        return null;
    }

    public string PadText(string textToPad, Pad pad, char textForPadding, int totalLength)
    {
        switch (pad)
        {
            case Pad.Left:
                return textToPad.PadLeft(totalLength, textForPadding);
                break;
            case Pad.Right:
                return textToPad.PadRight(totalLength, textForPadding);
                break;
        }

        return null;
    }

    public int ParseText(string textToParse, string textToFind, bool isRegularExpression,
        int startParsingAtPosition, bool firstOccurrenceOnly, bool ignoreCase)
    {
        var comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

        if (isRegularExpression)
        {
            var regexOptions = ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;
            var regex = new Regex(textToFind, regexOptions);
            var match = regex.Match(textToParse, startParsingAtPosition);

            if (match.Success)
                return match.Index;
        }
        else
        {
            return textToParse.IndexOf(textToFind, startParsingAtPosition, comparisonType);
        }

        return -1;
    }

    public string RecognizeEntitiesInText(string textToRecognizeFrom, EntityType entityType, Language language)
    {
        var minimumVaXXXXXXXXXXXXlue = await sandBox.EvaluateActionInput<int>(XXXXXXXXXXXX);
        var XXXXXXXXXXXX = await sandBox.EvaluateActionInput<int>(XXXXXXXXXXXX);

        XXXXXXXXXXXX.Value = new Random().Next(XXXXXXXXXXXX, XXXXXXXXXXXX);

        sandBox.SetVariable(XXXXXXXXXXXX);
    }

    public string ReplaceText(string textToParse, string textToFind,
        bool useRegularExpressionsForFindAndReplace, bool ignoreCase,
        string replaceWith, bool activeEscapeSequences)
    {
        if (activeEscapeSequences)
        {
            textToParse = EscapeSequences(textToParse);
            textToFind = EscapeSequences(textToFind);
            replaceWith = EscapeSequences(replaceWith);
        }

        if (useRegularExpressionsForFindAndReplace)
        {
            var regexOptions = ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;
            return Regex.Replace(textToParse, textToFind, replaceWith, regexOptions);
        }
        else
        {
            StringComparison comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            return textToParse.Replace(textToFind, replaceWith, comparison);
        }
    }

    private string EscapeSequences(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        return text.Replace("\\n", "\n")
            .Replace("\\t", "\t")
            .Replace("\\r", "\r")
            .Replace("\\b", "\b")
            .Replace("\\f", "\f")
            .Replace("\\a", "\a")
            .Replace("\\v", "\v")
            .Replace("\\\"", "\"")
            .Replace("\\'", "'")
            .Replace("\\\\", "\\");
    }

    public string ReverseText(string textToReverse)
    {
        return new string(textToReverse.Reverse().ToArray());
    }

    public List<string> SplitText(string textToSplit, SplitDelimiterType splitDelimiterType, StandardDelimiter standardDelimiter,
        int times, string customDelimiter, int splitWidth)
    {
        var result = new List<string>();

        switch (splitDelimiterType)
        {
            case SplitDelimiterType.Standard:
                string delimiter = "";

                switch (standardDelimiter)
                {
                    case StandardDelimiter.NewLine:
                        delimiter = Environment.NewLine;
                        break;
                    case StandardDelimiter.Space:
                        delimiter = " ";
                        break;
                    case StandardDelimiter.Tab:
                        delimiter = "\t";
                        break;
                }
                result.AddRange(textToSplit.Split(new[] { delimiter }, StringSplitOptions.None));
                break;

            case SplitDelimiterType.Custom:
                result.AddRange(textToSplit.Split(new[] { customDelimiter }, StringSplitOptions.None));
                break;

            case SplitDelimiterType.NumberOfCharacters:
                for (int i = 0; i < textToSplit.Length; i += splitWidth)
                {
                    result.Add(textToSplit.Substring(i, Math.Min(splitWidth, textToSplit.Length - i)));
                }
                break;
        }

        return result.GetRange(0, times);
    }

    public string TrimText(string textToTrim, WhatToTrim whatToTrim)
    {
        switch (whatToTrim)
        {
            case WhatToTrim.WhitespaceCharactersFromTheBeginning:
                return textToTrim.TrimStart();
                break;
            case WhatToTrim.WhitespaceCharactersFromTheBeginningAndEnd:
                return textToTrim.Trim();
                break;
            case WhatToTrim.WhitespaceCharactersFromTheEnd:
                return textToTrim.TrimEnd();
                break;
        }

        return null;
    }
}