using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Variable.Variable;

namespace FlowFusion.Action.Variable;

public class CreateNewList(IVariableService variableService) : GeneralAction
{
    public override string Name => "Create New List";

    // ReSharper disable once InconsistentNaming
    public string _Name { get; set; }

    public override async Task Execute(SandBox sandBox)
    {
        sandBox.SetVariable(new ListVariable(_Name, variableService.CreateNewList()));
        await Task.CompletedTask;
    }
}