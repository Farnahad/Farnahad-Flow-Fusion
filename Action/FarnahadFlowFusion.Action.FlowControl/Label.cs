using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.FlowControl;

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