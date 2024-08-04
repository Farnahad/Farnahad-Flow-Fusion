using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.Variable;

public class ClearList : IAction
{
    public string Name => "Clear List";

    public ActionInput ListToClear { get; set; }

    public ClearList()
    {
        ListToClear = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var list = await sandBox.EvaluateActionInput<List<object>>(ListToClear);

        list.Clear();
    }
}