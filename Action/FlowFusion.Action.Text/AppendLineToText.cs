using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;

namespace FlowFusion.Action.Text;

public class AppendLineToText(ITextService textService) : IAction
{
    public string Name => "Append line to text";

    public ActionInput OriginalText { get; set; } = new();
    public ActionInput LineToAppend { get; set; } = new();
    public Variable Result { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var originalTextValue = await sandBox.EvaluateActionInput<string>(OriginalText);
        var lineToAppendValue = await sandBox.EvaluateActionInput<string>(LineToAppend);

        Result.Value = textService.AppendLineToText(originalTextValue, lineToAppendValue);

        sandBox.SetVariable(Result);
    }
}