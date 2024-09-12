using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.FlowControl;

public class Region : IAction
{
    public string Name => "Region";

    // ReSharper disable once InconsistentNaming
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