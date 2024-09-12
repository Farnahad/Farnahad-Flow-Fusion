using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Variable.Variable;

namespace FlowFusion.Action.Variable;

public class RemoveDuplicateItemsFromList(IVariableService variableService) : GeneralAction
{
    public override string Name => "Remove Duplicate Items From List";

    public ActionInput ListToRemoveDuplicateItemsFrom { get; set; } = new();
    public bool IgnoreTextCaseWhileSearchingForDuplicateItems { get; set; }

    public override async Task Execute(SandBox sandBox)
    {
        var listToRemoveDuplicateItemsFromValue = await sandBox.EvaluateActionInput<List<object>>(ListToRemoveDuplicateItemsFrom);

        listToRemoveDuplicateItemsFromValue = variableService.RemoveDuplicateItemsFromList(
            listToRemoveDuplicateItemsFromValue, IgnoreTextCaseWhileSearchingForDuplicateItems);
    }
}