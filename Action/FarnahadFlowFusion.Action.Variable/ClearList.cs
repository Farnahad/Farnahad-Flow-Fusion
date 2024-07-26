using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Variable;

public class ClearList : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Clear List";

    public ActionInput ListToClear { get; set; }

    public ClearList()
    {
        _cSharpService = new CSharpService();

        ListToClear = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var list = await _cSharpService.EvaluateActionInput<List<object>>(sandBox, ListToClear);

        list.Clear();
    }
}