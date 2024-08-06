using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Text;

public class ConvertTextToDatetime : IAction //XXXXXXXXXXXX
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