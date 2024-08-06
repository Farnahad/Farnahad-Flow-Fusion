using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Variable;

public class CreateNewList : IAction
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