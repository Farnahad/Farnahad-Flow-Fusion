using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Variable.Variable;

namespace FlowFusion.Action.Variable;

public class AddItemToList(IVariableService variableService) : GeneralAction
{
    public override string Name => "Add Item to List";

    public ActionInput AddItem { get; set; } = new();
    public ActionInput InToList { get; set; } = new();

    public override async Task Execute(SandBox sandBox)
    {
        var list = await sandBox.EvaluateActionInput<List<object>>(InToList);
        var value = await sandBox.EvaluateActionInput<object>(AddItem);

        variableService.AddItemToList(list, value);
    }
}