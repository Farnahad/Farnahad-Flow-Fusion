using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.FlowControl;

public class GoTo : IAction
{
    public string Name => "Go to";

    public ActionInput GoToLabel { get; set; }

    public GoTo()
    {
        GoToLabel = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var goToLabelValue = await sandBox.EvaluateActionInput<string>(GoToLabel);
        sandBox.GoToLabel(goToLabelValue);

        await Task.CompletedTask;
    }
}