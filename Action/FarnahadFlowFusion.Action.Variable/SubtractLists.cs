using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Variable;

public class SubtractLists : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Subtract Lists";

    public ActionInput FirstList { get; set; }
    public ActionInput SecondList { get; set; }
    public Variable ListDifference { get; set; }

    public SubtractLists()
    {
        _cSharpService = new CSharpService();


        FirstList = new ActionInput();
        SecondList = new ActionInput();
        ListDifference = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var firstList = await _cSharpService.EvaluateActionInput<List<object>>(sandBox, FirstList);
        var secondList = await _cSharpService.EvaluateActionInput<List<object>>(sandBox, SecondList);

        ListDifference.Value = firstList.Except(secondList).ToList();
    }
}