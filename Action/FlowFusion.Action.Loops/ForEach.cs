using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Loops;

public class ForEach : IAction
{
    public string Name => "For each";

    public ActionInput ValueToIterate { get; set; }
    public Variable CurrentItem { get; set; }
    public List<IAction> Actions { get; set; }

    public ForEach()
    {
        ValueToIterate = new ActionInput();
        CurrentItem = new Variable();
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        var valueToIterateValues = await sandBox.EvaluateActionInput<List<object>>(ValueToIterate);

        foreach (var valueToIterate in valueToIterateValues)
        {
            CurrentItem.Value = valueToIterate;

            foreach (var action in Actions)
                await action.Execute(sandBox);
        }

        sandBox.SetVariable(CurrentItem);
    }
}