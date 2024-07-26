using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Variable;

public class RemoveDuplicateItemsFromList : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Remove Duplicate Items From List";

    public ActionInput ListToRemoveDuplicateItemsFrom { get; set; }
    public bool IgnoreTextCaseWhileSearchingForDuplicateItems { get; set; }

    public RemoveDuplicateItemsFromList()
    {
        _cSharpService = new CSharpService();

        ListToRemoveDuplicateItemsFrom = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var listToRemoveDuplicateItemsFrom = await _cSharpService.EvaluateActionInput<List<object>>(
            sandBox, ListToRemoveDuplicateItemsFrom);

        listToRemoveDuplicateItemsFrom = listToRemoveDuplicateItemsFrom
            .GroupBy(item => item).Select(g => g.First()).ToList();
    }
}