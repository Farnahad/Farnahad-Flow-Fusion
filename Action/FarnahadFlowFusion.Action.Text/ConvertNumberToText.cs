using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Text;

public class ConvertNumberToText : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Convert number to text";

    public ActionInput NumberToConvert { get; set; }
    public ActionInput DecimalPlaces { get; set; }
    public bool UserThousandsSeparator { get; set; }
    public Variable FormattedNumber { get; set; }

    public ConvertNumberToText()
    {
        _cSharpService = new CSharpService();

        NumberToConvert = new ActionInput();
        DecimalPlaces = new ActionInput();
        UserThousandsSeparator = true;
        FormattedNumber = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var numberToConvertValue = await _cSharpService.EvaluateActionInput<double>(sandBox, NumberToConvert);
        var decimalPlacesValue = await _cSharpService.EvaluateActionInput<double>(sandBox, DecimalPlaces);

        FormattedNumber.Value = UserThousandsSeparator ? numberToConvertValue.ToString("N" + decimalPlacesValue) :
            numberToConvertValue.ToString("F" + decimalPlacesValue);

        sandBox.Variables.Add(FormattedNumber);
    }
}