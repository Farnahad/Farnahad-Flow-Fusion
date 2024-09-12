using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.FlowControl;

public class End : GeneralAction
{
    public override string Name => "End";

    public End()
    {
    }

    public override async Task Execute(SandBox sandBox)
    {
        await sandBox.Stop();
    }
}