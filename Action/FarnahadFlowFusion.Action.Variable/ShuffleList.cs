using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Variable;

public class ShuffleList : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Shuffle List";

    public ActionInput ListToShuffle { get; set; }

    public ShuffleList()
    {
        _cSharpService = new CSharpService();

        ListToShuffle = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var list = await _cSharpService.EvaluateActionInput<List<object>>(sandBox, ListToShuffle);

        var random = new Random();
        var n = list.Count;

        for (var i = n - 1; i > 0; i--)
        {
            var j = random.Next(i + 1);

            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}