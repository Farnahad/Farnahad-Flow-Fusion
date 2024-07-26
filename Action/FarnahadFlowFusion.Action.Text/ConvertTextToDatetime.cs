using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Text;

public class ConvertTextToDatetime : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Convert text to datetime";

    public ActionInput TextToConvert { get; set; }
    public bool DateIsRepresentedInCustomFormat { get; set; }
    public Variable TextAsDateTime { get; set; }

    public ConvertTextToDatetime()
    {
        _cSharpService = new CSharpService();

        TextToConvert = new ActionInput();
        DateIsRepresentedInCustomFormat = false;
        TextAsDateTime = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToConvertValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToConvert);

        TextAsDateTime.Value = global::System.DateTime.Parse(textToConvertValue);

        sandBox.Variables.Add(TextAsDateTime);
    }
}