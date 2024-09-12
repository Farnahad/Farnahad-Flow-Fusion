using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Variable.Variable;

namespace FlowFusion.Action.Variable;

public class SortList(IVariableService variableService) : GeneralAction
{
    public override string Name => "Sort List";

    public ActionInput ListToSort { get; set; } = new();
    public bool SortByListItemsProperties { get; set; }

    public override async Task Execute(SandBox sandBox)
    {
        var listToSortValue = await sandBox.EvaluateActionInput<List<object>>(ListToSort);

        variableService.SortList(listToSortValue);
    }
}