using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Loops;

public class ForEach : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "For each";

    public ActionInput ValueToIterate { get; set; }
    public Variable CurrentItem { get; set; }
    public List<IAction> Actions { get; set; }

    public ForEach()
    {
        _cSharpService = new CSharpService();

        ValueToIterate = new ActionInput();
        CurrentItem = new Variable();
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        var valueToIterateValues = await _cSharpService.EvaluateActionInput<List<object>>(sandBox, ValueToIterate);

        foreach (var valueToIterate in valueToIterateValues)
        {
            CurrentItem.Value = valueToIterate;

            foreach (var action in Actions)
                await action.Execute(sandBox);
        }

        sandBox.Variables.Add(CurrentItem);
    }
}