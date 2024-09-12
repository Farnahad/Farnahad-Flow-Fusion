using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Variable.Variable;

namespace FlowFusion.Action.Variable;

public class ClearList(IVariableService variableService) : GeneralAction
{
    public override string Name => "Clear List";

    public ActionInput ListToClear { get; set; } = new();

    public override async Task Execute(SandBox sandBox)
    {
        var list = await sandBox.EvaluateActionInput<List<object>>(ListToClear);
        variableService.ClearList(list);
    }
}