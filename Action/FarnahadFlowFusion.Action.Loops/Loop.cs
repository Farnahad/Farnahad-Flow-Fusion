using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Loops;

public class Loop : IAction
{
    public string Name => "Loop";

    public ActionInput StartFrom { get; set; }
    public ActionInput EndTo { get; set; }
    public ActionInput IncrementBy { get; set; }
    public Variable LoopIndex { get; set; }
    public List<IAction> Actions { get; set; }

    public Loop()
    {
        StartFrom = new ActionInput();
        EndTo = new ActionInput();
        IncrementBy = new ActionInput();
        LoopIndex = new Variable();
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        var startFromValue = await sandBox.EvaluateActionInput<int>(StartFrom);
        var endToValue = await sandBox.EvaluateActionInput<int>(EndTo);
        var incrementByValue = await sandBox.EvaluateActionInput<int>(IncrementBy);

        for (double i = startFromValue; i < endToValue; i += incrementByValue)
        {
            LoopIndex.Value = i;

            foreach (var action in Actions)
                await action.Execute(sandBox);
        }

        sandBox.SetVariable(LoopIndex);
    }
}