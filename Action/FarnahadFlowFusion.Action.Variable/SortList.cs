using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Variable;

public class SortList : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Sort List";

    public ActionInput ListToSort { get; set; }
    public bool SortByListItemsProperties { get; set; }

    public SortList()
    {
        _cSharpService = new CSharpService();

        ListToSort = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var listToSort = await _cSharpService.EvaluateActionInput<List<object>>(sandBox, ListToSort);

        listToSort.Sort();
    }
}