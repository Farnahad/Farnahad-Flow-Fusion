using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.FlowControl;

public class Region : IAction
{
    public string Name => "Region";

    public string _Name { get; set; }

    public Region()
    {
        _Name = "";
    }

    public async Task Execute(SandBox sandBox)
    {
        await Task.CompletedTask;
    }
}