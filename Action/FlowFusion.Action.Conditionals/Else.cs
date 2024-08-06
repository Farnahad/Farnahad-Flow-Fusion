using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Conditionals;

public class Else : IAction
{
    public string Name => "Else";

    public List<IAction> Actions { get; set; }

    public Else()
    {
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        foreach (var action in Actions)
            await action.Execute(sandBox);
    }
}