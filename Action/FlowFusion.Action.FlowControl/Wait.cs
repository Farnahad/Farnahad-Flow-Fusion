using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.FlowControl.FlowControl;

namespace FlowFusion.Action.FlowControl;

public class Wait(IFlowControlService flowControlService) : GeneralAction
{
    public override string Name => "Wait";

    public ActionInput Duration { get; set; } = new();

    public override async Task Execute(SandBox sandBox)
    {
        var durationValue = await sandBox.EvaluateActionInput<int>(Duration);
        await flowControlService.Wait(durationValue);
    }
}