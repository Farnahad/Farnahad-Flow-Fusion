using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Variable.Variable;

namespace FlowFusion.Action.Variable;

public class MergeLists(IVariableService variableService) : GeneralAction
{
    public override string Name => "Merge Lists";

    public ActionInput FirstList { get; set; } = new();
    public ActionInput SecondList { get; set; } = new();
    public Main.Variable.Variable OutputList { get; set; } = new();

    public override async Task Execute(SandBox sandBox)
    {
        var firstListValue = await sandBox.EvaluateActionInput<List<object>>(FirstList);
        var secondListValue = await sandBox.EvaluateActionInput<List<object>>(SecondList);

        OutputList.Value = variableService.MergeLists(firstListValue, secondListValue);
        sandBox.SetVariable(OutputList);
    }
}