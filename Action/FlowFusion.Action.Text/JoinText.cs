using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;
using FlowFusion.Service.Text.Text.Base;

namespace FlowFusion.Action.Text;

public class JoinText(ITextService textService) : IAction
{
    public string Name => "Join text";

    public ActionInput SpecifyTheListToJoin { get; set; } = new();
    public JoinDelimiterToSeparateListItems DelimiterToSeparateListItems { get; set; } = JoinDelimiterToSeparateListItems.None;
    public StandardDelimiter StandardDelimiter { get; set; }
    public ActionInput CustomDelimiter { get; set; }
    public Variable JoinedText { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var specifyTheListToJoinValue = await sandBox.EvaluateActionInput<List<string>>(SpecifyTheListToJoin);
        var customDelimiterValue = await sandBox.EvaluateActionInput<string>(CustomDelimiter);

        JoinedText.Value = textService.JoinText(specifyTheListToJoinValue, DelimiterToSeparateListItems,
            StandardDelimiter, customDelimiterValue);

        sandBox.SetVariable(JoinedText);
    }
}