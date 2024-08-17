using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;
using FlowFusion.Service.Text.Text.Base;

namespace FlowFusion.Action.Text;

public class ConvertDatetimeToText(ITextService textService) : IAction
{
    public string Name => "Convert datetime to text";

    public ActionInput DatetimeToConvert { get; set; } = new();
    public DateTimeFormatToUse FormatToUse { get; set; } = DateTimeFormatToUse.Standard;
    public DateTimeStandardFormat StandardFormat { get; set; } = DateTimeStandardFormat.ShortDate;
    public ActionInput CustomFormat { get; set; } = new();
    public Variable FormattedDateTime { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var datetimeToConvertValue = await sandBox.EvaluateActionInput<global::System.DateTime>(DatetimeToConvert);
        var customFormatValue = await sandBox.EvaluateActionInput<string>(DatetimeToConvert);

        FormattedDateTime.Value = textService.ConvertDatetimeToText(
            datetimeToConvertValue, FormatToUse, StandardFormat, customFormatValue);

        sandBox.SetVariable(FormattedDateTime);
    }
}