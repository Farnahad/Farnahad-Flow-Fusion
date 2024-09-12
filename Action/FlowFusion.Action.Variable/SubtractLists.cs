using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Variable.Variable;

namespace FlowFusion.Action.Variable;

public class SubtractLists(IVariableService variableService) : GeneralAction
{
    public override string Name => "Subtract Lists";

    public ActionInput FirstList { get; set; } = new();
    public ActionInput SecondList { get; set; } = new();
    public Main.Variable.Variable ListDifference { get; set; } = new();

    public override async Task Execute(SandBox sandBox)
    {
        var firstListValue = await sandBox.EvaluateActionInput<List<object>>(FirstList);
        var secondListValue = await sandBox.EvaluateActionInput<List<object>>(SecondList);

        ListDifference.Value = variableService.SubtractLists(firstListValue, secondListValue);
    }
}