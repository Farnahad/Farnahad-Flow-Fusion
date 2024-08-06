using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.FlowControl;

public class Wait : IAction //XXXXXXXXXXXX
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