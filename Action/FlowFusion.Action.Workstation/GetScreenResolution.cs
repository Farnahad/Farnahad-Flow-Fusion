using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

public class GetScreenResolution(IWorkstationService workstationService) : IAction
{
    public string Name => "Get screen resolution";

    public ActionInput MonitorNumber { get; set; } = new();
    public Variable MonitorWidth { get; set; } = new();
    public Variable MonitorHeight { get; set; } = new();
    public Variable MonitorBitCount { get; set; } = new();
    public Variable MonitorFrequency { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var monitorNumberValue = await sandBox.EvaluateActionInput<int>(MonitorNumber);

        var resolution = workstationService.GetScreenResolution(monitorNumberValue);

        MonitorWidth.Value = resolution.Item1;
        MonitorHeight.Value = resolution.Item2;
        MonitorBitCount.Value = resolution.Item3;
        MonitorFrequency.Value = resolution.Item4;
    }
}