using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Variable;

public class AddItemToList : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Add Item to List";

    public ActionInput AddItem { get; set; }
    public ActionInput InToList { get; set; }

    public AddItemToList()
    {
        _cSharpService = new CSharpService();

        AddItem = new ActionInput();
        InToList = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var list = await _cSharpService.EvaluateActionInput<List<object>>(sandBox, InToList);
        var value = await _cSharpService.EvaluateActionInput<object>(sandBox, AddItem);

        list.Add(value);
    }
}