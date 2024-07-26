using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Variable;

public class CreateNewList : IAction
{
    public string Name => "Create New List";

    // ReSharper disable once InconsistentNaming
    public string _Name { get; set; }

    public async Task Execute(SandBox sandBox)
    {
        sandBox.Variables.Add(new ListVariable(_Name, new List<object>()));
        await Task.CompletedTask;
    }
}