using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Variable;

public class SortList : IAction //XXXXXXXXXXXX
{
    public string Name => "Sort List";

    public ActionInput ListToSort { get; set; }
    public bool SortByListItemsProperties { get; set; }

    public SortList()
    {
        ListToSort = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var listToSort = await sandBox.EvaluateActionInput<List<object>>(ListToSort);

        listToSort.Sort();
    }
}