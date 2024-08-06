using System.Text;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Text;

public class AppendLineToText : IAction
{
    public string Name => "Append line to text";

    public ActionInput OriginalText { get; set; }
    public ActionInput LineToAppend { get; set; }
    public Variable Result { get; set; }

    public AppendLineToText()
    {
        OriginalText = new ActionInput();
        LineToAppend = new ActionInput();
        Result = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var originalTextValue = await sandBox.EvaluateActionInput<string>(OriginalText);
        var lineToAppendValue = await sandBox.EvaluateActionInput<string>(LineToAppend);

        var stringBuilder = new StringBuilder(originalTextValue);
        stringBuilder.AppendLine(lineToAppendValue);
        Result.Value = stringBuilder.ToString();

        sandBox.SetVariable(Result);
    }
}