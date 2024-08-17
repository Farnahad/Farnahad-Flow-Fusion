using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;
using FlowFusion.Service.Text.Text.Base;

namespace FlowFusion.Action.Text;

public class SplitText(ITextService textService) : IAction
{
    public string Name => "Split text";

    public ActionInput TextToSplit { get; set; } = new();
    public SplitDelimiterType DelimiterType { get; set; } = SplitDelimiterType.Standard;
    public StandardDelimiter StandardDelimiter { get; set; } = StandardDelimiter.Space;
    public ActionInput Times { get; set; } = new();
    public ActionInput CustomDelimiter { get; set; } = new();
    public ActionInput SplitWidth { get; set; } = new();
    public Variable TextList { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToSplitValue = await sandBox.EvaluateActionInput<string>(TextToSplit);
        var timesValue = await sandBox.EvaluateActionInput<int>(Times);
        var customDelimiterValue = await sandBox.EvaluateActionInput<string>(CustomDelimiter);
        var splitWidthValue = await sandBox.EvaluateActionInput<int>(SplitWidth);

        TextList.Value = textService.SplitText(textToSplitValue, DelimiterType,
            StandardDelimiter, timesValue, customDelimiterValue, splitWidthValue);

        sandBox.SetVariable(TextList);
    }
}