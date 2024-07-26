using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Conditionals;

public class DefaultCase : IAction
{
    public string Name => "Default Case";

    public List<IAction> Actions { get; set; }

    public DefaultCase()
    {
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        foreach (var action in Actions)
            await action.Execute(sandBox);
    }
}