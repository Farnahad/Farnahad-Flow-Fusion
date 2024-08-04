using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Variable.RemoveItemFromListBase;

namespace FarnahadFlowFusion.Action.Variable;

public class RemoveItemFromList : IAction
{
    public string Name => "Remove Item from List";

    public RemoveItemBy RemoveItemBy { get; set; }
    public ActionInput AtIndex { get; set; }
    public ActionInput WithValue { get; set; }
    public bool RemoveAllItemOccurrences { get; set; }
    public ActionInput FromList { get; set; }

    public RemoveItemFromList()
    {
        RemoveItemBy = RemoveItemBy.Index;
        AtIndex = new ActionInput();
        WithValue = new ActionInput();
        FromList = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var fromList = await sandBox.EvaluateActionInput<List<object>>(FromList);

        switch (RemoveItemBy)
        {
            case RemoveItemBy.Index:
                var atIndex = await sandBox.EvaluateActionInput<int>(AtIndex);
                fromList.RemoveAt(atIndex);
                break;
            case RemoveItemBy.Value:
                var withValue = await sandBox.EvaluateActionInput<object>(WithValue);
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