using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Variable;

public class CreateNewList : IAction //XXXXXXXXXXXX
{
    public string Name => "Create New List";

    // ReSharper disable once InconsistentNaming
    public string _Name { get; set; }

    public async Task Execute(SandBox sandBox)
    {
        sandBox.SetVariable(new ListVariable(_Name, new List<object>()));
        await Task.CompletedTask;
    }
}