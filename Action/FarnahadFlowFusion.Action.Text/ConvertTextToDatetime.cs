using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Text;

public class ConvertTextToDatetime : IAction
{
    public string Name => "Convert text to datetime";

    public ActionInput TextToConvert { get; set; }
    public bool DateIsRepresentedInCustomFormat { get; set; }
    public Variable TextAsDateTime { get; set; }

    public ConvertTextToDatetime()
    {
        TextToConvert = new ActionInput();
        DateIsRepresentedInCustomFormat = false;
        TextAsDateTime = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToConvertValue = await sandBox.EvaluateActionInput<string>(TextToConvert);

        TextAsDateTime.Value = global::System.DateTime.Parse(textToConvertValue);

        sandBox.SetVariable(TextAsDateTime);
    }
}