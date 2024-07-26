using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Variable;

public class ReverseList : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Reverse List";

    public ActionInput ListToReverse { get; set; }

    public ReverseList()
    {
        _cSharpService = new CSharpService();

        ListToReverse = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var listToReverse = await _cSharpService.EvaluateActionInput<List<object>>(sandBox, ListToReverse);

        listToReverse.Reverse();
    }
}