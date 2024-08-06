using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Variable;

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