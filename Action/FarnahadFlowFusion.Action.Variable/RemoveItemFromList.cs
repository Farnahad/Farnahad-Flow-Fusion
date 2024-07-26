using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Variable.RemoveItemFromListBase;

namespace FarnahadFlowFusion.Action.Variable;

public class RemoveItemFromList : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Remove Item from List";

    public RemoveItemBy RemoveItemBy { get; set; }
    public ActionInput AtIndex { get; set; }
    public ActionInput WithValue { get; set; }
    public bool RemoveAllItemOccurrences { get; set; }
    public ActionInput FromList { get; set; }

    public RemoveItemFromList()
    {
        _cSharpService = new CSharpService();

        RemoveItemBy = RemoveItemBy.Index;
        AtIndex = new ActionInput();
        WithValue = new ActionInput();
        FromList = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var fromList = await _cSharpService.EvaluateActionInput<List<object>>(sandBox, FromList);

        switch (RemoveItemBy)
        {
            case RemoveItemBy.Index:
                var atIndex = await _cSharpService.EvaluateActionInput<int>(sandBox, AtIndex);
                fromList.RemoveAt(atIndex);
                break;
            case RemoveItemBy.Value:
                var withValue = await _cSharpService.EvaluateActionInput<object>(sandBox, WithValue);
                if (RemoveAllItemOccurrences)
                {
                    var removeItems = fromList.Where(item => item == withValue);
                    foreach (var removeItem in removeItems)
                        fromList.Remove(removeItem);

                }
                else
                {
                    fromList.Remove(withValue);
                }
                break;
        }
    }
}