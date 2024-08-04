using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.FlowControl;

public class Wait : IAction
{
    public string Name => "Wait";

    public ActionInput Duration { get; set; }

    public Wait()
    {
        Duration = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var durationValue = await sandBox.EvaluateActionInput<int>(Duration);
        await Task.Delay(TimeSpan.FromSeconds(durationValue));
    }
}