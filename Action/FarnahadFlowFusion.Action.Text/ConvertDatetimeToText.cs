using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Text.ConvertDatetimeToTextBase;

namespace FarnahadFlowFusion.Action.Text;

public class ConvertDatetimeToText : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Convert datetime to text";

    public ActionInput DatetimeToConvert { get; set; }
    public FormatToUse FormatToUse { get; set; }
    public StandardFormat StandardFormat { get; set; }
    public ActionInput CustomFormat { get; set; }
    public Variable FormattedDateTime { get; set; }

    public ConvertDatetimeToText()
    {
        _cSharpService = new CSharpService();

        DatetimeToConvert = new ActionInput();
        FormatToUse = FormatToUse.Standard;
        StandardFormat = StandardFormat.ShortDate;
        CustomFormat = new ActionInput();
        FormattedDateTime = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var datetimeToConvertValue = await _cSharpService.EvaluateActionInput<global::System.DateTime>(sandBox, DatetimeToConvert);

        if (FormatToUse == FormatToUse.Custom)
        {
            var customFormat = await _cSharpService.EvaluateActionInput<string>(sandBox, CustomFormat);
            FormattedDateTime.Value = datetimeToConvertValue.ToString(customFormat);
        }
        else if (FormatToUse == FormatToUse.Standard)
        {
            FormattedDateTime.Value = StandardFormat switch
            {
                StandardFormat.FullDatetimeLongTime => datetimeToConvertValue.ToString("F"),
                StandardFormat.FullDatetimeShortTime => datetimeToConvertValue.ToString("f"),
                StandardFormat.GeneralDatetimeLongTime => datetimeToConvertValue.ToString("G"),
                StandardFormat.GeneralDatetimeShortTime => datetimeToConvertValue.ToString("g"),
                StandardFormat.LongDate => datetimeToConvertValue.ToString("D"),
                StandardFormat.LongTime => datetimeToConvertValue.ToString("T"),
                StandardFormat.ShortDate => datetimeToConvertValue.ToString("d"),
                StandardFormat.ShortTime => datetimeToConvertValue.ToString("t"),
                StandardFormat.SortableDatetime => datetimeToConvertValue.ToString("s"),
                _ => FormattedDateTime.Value
            };
        }

        sandBox.Variables.Add(FormattedDateTime);
    }
}