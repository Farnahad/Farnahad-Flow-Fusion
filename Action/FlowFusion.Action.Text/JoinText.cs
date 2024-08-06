using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Action.Text.JoinTextBase;

namespace FarnahadFlowFusion.Action.Text;

public class JoinText : IAction
{
    public string Name => "Join text";

    public ActionInput SpecifyTheListToJoin { get; set; }
    public DelimiterToSeparateListItems DelimiterToSeparateListItems { get; set; }
    public StandardDelimiter StandardDelimiter { get; set; }
    public ActionInput CustomDelimiter { get; set; }
    public Variable JoinedText { get; set; }

    public JoinText()
    {
        SpecifyTheListToJoin = new ActionInput();
        DelimiterToSeparateListItems = DelimiterToSeparateListItems.None;
        JoinedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var specifyTheListToJoinValue = await sandBox.EvaluateActionInput<List<string>>(SpecifyTheListToJoin);

        switch (DelimiterToSeparateListItems)
        {
            case DelimiterToSeparateListItems.Custom:
                var customDelimiterValue = await sandBox.EvaluateActionInput<string>(CustomDelimiter);
                JoinedText.Value = string.Join(customDelimiterValue, specifyTheListToJoinValue);
                break;
            case DelimiterToSeparateListItems.None:
                JoinedText.Value = string.Join("", specifyTheListToJoinValue);
                break;
            case DelimiterToSeparateListItems.Standard:

                switch (StandardDelimiter)
                {
                    case StandardDelimiter.NewLine:
                        JoinedText.Value = string.Join(Environment.NewLine, specifyTheListToJoinValue);
                        break;
                    case StandardDelimiter.Space:
                        JoinedText.Value = string.Join(" ", specifyTheListToJoinValue);
                        break;
                    case StandardDelimiter.Tab:
                        JoinedText.Value = string.Join("\t", specifyTheListToJoinValue);
                        break;
                }
                break;
        }

        sandBox.SetVariable(JoinedText);
    }
}