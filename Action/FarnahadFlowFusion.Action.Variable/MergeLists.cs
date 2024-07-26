using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Variable;

public class MergeLists : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Merge Lists";

    public ActionInput FirstList { get; set; }
    public ActionInput SecondList { get; set; }
    public Variable OutputList { get; set; }

    public MergeLists()
    {
        _cSharpService = new CSharpService();

        FirstList = new ActionInput();
        SecondList = new ActionInput();
        OutputList = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var firstList = await _cSharpService.EvaluateActionInput<List<object>>(sandBox, FirstList);
        var secondList = await _cSharpService.EvaluateActionInput<List<object>>(sandBox, SecondList);

        OutputList.Value = firstList.Union(secondList).ToList();
        sandBox.Variables.Add(OutputList);
    }
}