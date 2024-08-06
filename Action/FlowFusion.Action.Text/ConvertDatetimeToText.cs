using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Action.Text.ConvertDatetimeToTextBase;

namespace FarnahadFlowFusion.Action.Text;

public class ConvertDatetimeToText : IAction
{
    public string Name => "Convert datetime to text";

    public ActionInput DatetimeToConvert { get; set; }
    public FormatToUse FormatToUse { get; set; }
    public StandardFormat StandardFormat { get; set; }
    public ActionInput CustomFormat { get; set; }
    public Variable FormattedDateTime { get; set; }

    public ConvertDatetimeToText()
    {
        DatetimeToConvert = new ActionInput();
        FormatToUse = FormatToUse.Standard;
        StandardFormat = StandardFormat.ShortDate;
        CustomFormat = new ActionInput();
        FormattedDateTime = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var datetimeToConvertValue = await sandBox.EvaluateActionInput<global::System.DateTime>(DatetimeToConvert);

        if (FormatToUse == FormatToUse.Custom)
        {
            var customFormat = await sandBox.EvaluateActionInput<string>(CustomFormat);
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

        sandBox.SetVariable(FormattedDateTime);
    }
}