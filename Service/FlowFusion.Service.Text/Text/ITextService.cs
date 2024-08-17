using FlowFusion.Service.Text.Text.Base;

namespace FlowFusion.Service.Text.Text;

public interface ITextService
{
    string AppendLineToText(string originalText, string lineToAppend);
    string ChangeTextCase(string textToConvert, CaseConvertTo convertTo);
    string ConvertDatetimeToText(DateTime datetimeToConvert, DateTimeFormatToUse dateTimeFormatToUse,
        DateTimeStandardFormat dateTimeStandardFormat, string customFormat);
    string ConvertNumberToText(double numberToConvert, int decimalPlaces, bool userThousandsSeparator);
    DateTime ConvertTextToDatetime(string textToConvert, bool dateIsRepresentedInCustomFormat);
    double ConvertTextToNumber(string textToConvert);
    string CreateHtmlContent(string html);
    string CreateRandomText(bool userUppercaseLetters, bool userLowercaseLetters,
        bool useDigits, bool useSymbols, int minimumLength, int maximumLength);
    (string, bool) CropText(string originalText, CropMode cropMode, string endFlag, bool ignoreCase);
    string EscapeTextForRegularExpression(string textToEscape);
    string GetSubtext(string originalText, SubtextStartIndex subtextStartIndex, int characterPosition,
        SubtextLength subtextLength, int numberOfChars);
    string JoinText(List<string> specifyTheListToJoin, JoinDelimiterToSeparateListItems joinDelimiterToSeparateListItems,
        StandardDelimiter standardDelimiter, string customDelimiter);
    string PadText(string textToPad, Pad pad, char textForPadding, int totalLength);
    int ParseText(string textToParse, string textToFind, bool isRegularExpression,
        int startParsingAtPosition, bool firstOccurrenceOnly, bool ignoreCase);
    string RecognizeEntitiesInText(string textToRecognizeFrom, EntityType entityType, Language language);
    string ReplaceText(string textToParse, string textToFind,
        bool useRegularExpressionsForFindAndReplace, bool ignoreCase, string replaceWith,
        bool activeEscapeSequences);
    string ReverseText(string textToReverse);
    List<string> SplitText(string textToSplit, SplitDelimiterType splitDelimiterType, StandardDelimiter standardDelimiter,
        int times, string customDelimiter, int splitWidth);
    string TrimText(string textToTrim, WhatToTrim whatToTrim);
}