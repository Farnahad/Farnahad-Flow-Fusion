using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Variable;

public class MergeLists : IAction
{
    public string Name => "Merge Lists";

    public ActionInput FirstList { get; set; }
    public ActionInput SecondList { get; set; }
    public Main.Variable.Variable OutputList { get; set; }

    public MergeLists()
    {
        FirstList = new ActionInput();
        SecondList = new ActionInput();
        OutputList = new Main.Variable.Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var firstList = await sandBox.EvaluateActionInput<List<object>>(FirstList);
        var secondList = await sandBox.EvaluateActionInput<List<object>>(SecondList);

        OutputList.Value = firstList.Union(secondList).ToList();
        sandBox.SetVariable(OutputList);
    }
}