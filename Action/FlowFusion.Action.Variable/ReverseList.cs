using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Variable.Variable;

namespace FlowFusion.Action.Variable;

public class ReverseList(IVariableService variableService) : GeneralAction
{
    public override string Name => "Reverse List";

    public ActionInput ListToReverse { get; set; } = new();

    public override async Task Execute(SandBox sandBox)
    {
        var listToReverseValue = await sandBox.EvaluateActionInput<List<object>>(ListToReverse);

        variableService.ReverseList(listToReverseValue);
    }
}