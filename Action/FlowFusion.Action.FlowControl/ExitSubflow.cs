using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.FlowControl;

public class ExitSubflow : GeneralAction
{
    public override string Name => "Exit Subflow";

    public ExitSubflow()
    {
    }

    public override async Task Execute(SandBox sandBox)
    {
        sandBox.ExitSubflow();
        await Task.CompletedTask;
    }
}