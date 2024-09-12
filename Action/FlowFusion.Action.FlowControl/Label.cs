using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.FlowControl;

public class Label : IAction
{
    public string Name => "Label";

    public string LabelName { get; set; }

    public Label()
    {
        LabelName = "";
    }

    public async Task Execute(SandBox sandBox)
    {
        await Task.CompletedTask;
    }
}