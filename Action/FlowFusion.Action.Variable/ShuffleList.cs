using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Variable;

public class ShuffleList : IAction
{
    public string Name => "Shuffle List";

    public ActionInput ListToShuffle { get; set; }

    public ShuffleList()
    {
        ListToShuffle = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var list = await sandBox.EvaluateActionInput<List<object>>(ListToShuffle);

        var random = new Random();
        var n = list.Count;

        for (var i = n - 1; i > 0; i--)
        {
            var j = random.Next(i + 1);

            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}