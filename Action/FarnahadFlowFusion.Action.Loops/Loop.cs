using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Loops;

public class Loop : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Loop";

    public ActionInput StartFrom { get; set; }
    public ActionInput EndTo { get; set; }
    public ActionInput IncrementBy { get; set; }
    public Variable LoopIndex { get; set; }
    public List<IAction> Actions { get; set; }

    public Loop()
    {
        _cSharpService = new CSharpService();

        StartFrom = new ActionInput();
        EndTo = new ActionInput();
        IncrementBy = new ActionInput();
        LoopIndex = new Variable();
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        var startFromValue = await _cSharpService.EvaluateActionInput<int>(sandBox, StartFrom);
        var endToValue = await _cSharpService.EvaluateActionInput<int>(sandBox, EndTo);
        var incrementByValue = await _cSharpService.EvaluateActionInput<int>(sandBox, IncrementBy);

        for (double i = startFromValue; i < endToValue; i += incrementByValue)
        {
            LoopIndex.Value = i;

            foreach (var action in Actions)
                await action.Execute(sandBox);
        }

        sandBox.Variables.Add(LoopIndex);
    }
}