using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Workstation.Workstation;

namespace FarnahadFlowFusion.Action.Workstation;

public class GetScreenResolution : IAction
{
    private readonly WorkstationService _workstationService;

    public string Name => "Get screen resolution";

    public ActionInput MonitorNumber { get; set; }
    public Variable MonitorWidth { get; set; }
    public Variable MonitorHeight { get; set; }
    public Variable MonitorBitCount { get; set; }
    public Variable MonitorFrequency { get; set; }

    public GetScreenResolution()
    {
        _workstationService = new WorkstationService();

        MonitorNumber = new ActionInput();
        MonitorWidth = new Variable();
        MonitorHeight = new Variable();
        MonitorBitCount = new Variable();
        MonitorFrequency = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var monitorNumberValue = await sandBox.EvaluateActionInput<int>(MonitorNumber);

        _workstationService.GetScreenResolution(monitorNumberValue,
            out var monitorWidth, out var monitorHeight,out var monitorBitCount, out var monitorFrequency);

        MonitorWidth.Value = monitorWidth;
        MonitorHeight.Value = monitorHeight;
        MonitorBitCount.Value = monitorBitCount;
        MonitorFrequency.Value = monitorFrequency;
    }
}