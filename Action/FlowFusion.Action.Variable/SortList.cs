using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.Variable;

public class SortList : IAction
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