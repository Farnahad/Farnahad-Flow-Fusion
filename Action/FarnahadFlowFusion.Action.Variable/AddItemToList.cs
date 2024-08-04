using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.Variable;

public class AddItemToList : IAction
{
    public string Name => "Add Item to List";

    public ActionInput AddItem { get; set; }
    public ActionInput InToList { get; set; }

    public AddItemToList()
    {
        AddItem = new ActionInput();
        InToList = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var list = await sandBox.EvaluateActionInput<List<object>>(InToList);
        var value = await sandBox.EvaluateActionInput<object>(AddItem);

        list.Add(value);
    }
}