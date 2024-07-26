using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.FlowControl;

public class GoTo : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Go to";

    public ActionInput GoToLabel { get; set; }

    public GoTo()
    {
        _cSharpService = new CSharpService();

        GoToLabel = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var goToLabelValue = await _cSharpService.EvaluateActionInput<string>(sandBox, GoToLabel);

        var label = sandBox.WorkFlow.Actions.Where(item => item.GetType() == typeof(Label))
            .FirstOrDefault(item => item.Name == goToLabelValue);

        if (label != null)
            sandBox.CurrentAction = label;

        await Task.CompletedTask;
    }
}