using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

public class SetScreenResolution(IWorkstationService workstationService) : IAction
{
    public string Name => "Set screen resolution";

    public ActionInput MonitorNumber { get; set; } = new();
    public ActionInput MonitorWidth { get; set; } = new();
    public ActionInput MonitorHeight { get; set; } = new();
    public ActionInput MonitorBitCount { get; set; } = new();
    public ActionInput MonitorFrequency { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var monitorNumberValue = await sandBox.EvaluateActionInput<int>(MonitorNumber);
        var monitorWidthValue = await sandBox.EvaluateActionInput<int>(MonitorWidth);
        var monitorHeightValue = await sandBox.EvaluateActionInput<int>(MonitorHeight);
        var monitorBitCountValue = await sandBox.EvaluateActionInput<int>(MonitorBitCount);
        var monitorFrequencyValue = await sandBox.EvaluateActionInput<int>(MonitorFrequency);

        workstationService.SetScreenResolution(monitorNumberValue,
            monitorWidthValue, monitorHeightValue, monitorBitCountValue, monitorFrequencyValue);
    }
}