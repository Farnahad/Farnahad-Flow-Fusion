using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Variable.RemoveItemFromListBase;
using FlowFusion.Service.Variable.Variable;

namespace FlowFusion.Action.Variable;

public class RemoveItemFromList(IVariableService variableService) : GeneralAction
{
    public override string Name => "Remove Item from List";

    public RemoveItemBy RemoveItemBy { get; set; } = RemoveItemBy.Index;
    public ActionInput AtIndex { get; set; } = new();
    public ActionInput WithValue { get; set; } = new();
    public bool RemoveAllItemOccurrences { get; set; }
    public ActionInput FromList { get; set; } = new();

    public override async Task Execute(SandBox sandBox)
    {
        var atIndexValue = await sandBox.EvaluateActionInput<int>(AtIndex);
        var withValueValue = await sandBox.EvaluateActionInput<object>(WithValue);
        var fromListValue = await sandBox.EvaluateActionInput<List<object>>(FromList);

        variableService.RemoveItemFromList(RemoveItemBy, atIndexValue,
            withValueValue, RemoveAllItemOccurrences, fromListValue);
    }
}