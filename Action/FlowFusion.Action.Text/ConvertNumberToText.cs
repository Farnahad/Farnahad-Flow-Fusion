using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;

namespace FlowFusion.Action.Text;

public class ConvertNumberToText(ITextService textService) : IAction
{
    public string Name => "Convert number to text";

    public ActionInput NumberToConvert { get; set; } = new();
    public ActionInput DecimalPlaces { get; set; } = new();
    public bool UserThousandsSeparator { get; set; } = true;
    public Variable FormattedNumber { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var numberToConvertValue = await sandBox.EvaluateActionInput<double>(NumberToConvert);
        var decimalPlacesValue = await sandBox.EvaluateActionInput<int>(DecimalPlaces);

        FormattedNumber.Value = textService.ConvertNumberToText(numberToConvertValue, decimalPlacesValue, UserThousandsSeparator);

        sandBox.SetVariable(FormattedNumber);
    }
}