using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.Variable;

public class SubtractLists : IAction
{
    public string Name => "Subtract Lists";

    public ActionInput FirstList { get; set; }
    public ActionInput SecondList { get; set; }
    public Main.Variable.Variable ListDifference { get; set; }

    public SubtractLists()
    {
        FirstList = new ActionInput();
        SecondList = new ActionInput();
        ListDifference = new Main.Variable.Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var firstList = await sandBox.EvaluateActionInput<List<object>>(FirstList);
        var secondList = await sandBox.EvaluateActionInput<List<object>>(SecondList);

        ListDifference.Value = firstList.Except(secondList).ToList();
    }
}