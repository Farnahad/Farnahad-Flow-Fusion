using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Variable.Variable;

namespace FlowFusion.Action.Variable;

public class ShuffleList(IVariableService variableService) : GeneralAction
{
    public override string Name => "Shuffle List";

    public ActionInput ListToShuffle { get; set; } = new();

    public override async Task Execute(SandBox sandBox)
    {
        var listToShuffleValue = await sandBox.EvaluateActionInput<List<object>>(ListToShuffle);

        variableService.ShuffleList(listToShuffleValue);
    }
}