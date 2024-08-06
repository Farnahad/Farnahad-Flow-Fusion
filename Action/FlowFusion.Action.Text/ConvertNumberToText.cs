using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Text;

public class ConvertNumberToText : IAction
{
    public string Name => "Convert number to text";

    public ActionInput NumberToConvert { get; set; }
    public ActionInput DecimalPlaces { get; set; }
    public bool UserThousandsSeparator { get; set; }
    public Variable FormattedNumber { get; set; }

    public ConvertNumberToText()
    {
        NumberToConvert = new ActionInput();
        DecimalPlaces = new ActionInput();
        UserThousandsSeparator = true;
        FormattedNumber = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var numberToConvertValue = await sandBox.EvaluateActionInput<double>(NumberToConvert);
        var decimalPlacesValue = await sandBox.EvaluateActionInput<double>(DecimalPlaces);

        FormattedNumber.Value = UserThousandsSeparator ? numberToConvertValue.ToString("N" + decimalPlacesValue) :
            numberToConvertValue.ToString("F" + decimalPlacesValue);

        sandBox.SetVariable(FormattedNumber);
    }
}