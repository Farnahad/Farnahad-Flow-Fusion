using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

public class SetScreenResolution : IAction
{
    private readonly WorkstationService _workstationService;

    public string Name => "Set screen resolution";

    public ActionInput MonitorNumber { get; set; }
    public ActionInput MonitorWidth { get; set; }
    public ActionInput MonitorHeight { get; set; }
    public ActionInput MonitorBitCount { get; set; }
    public ActionInput MonitorFrequency { get; set; }

    public SetScreenResolution()
    {
        _workstationService = new WorkstationService();

        MonitorNumber = new ActionInput();
        MonitorWidth = new ActionInput();
        MonitorHeight = new ActionInput();
        MonitorBitCount = new ActionInput();
        MonitorFrequency = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var monitorNumberValue = await sandBox.EvaluateActionInput<int>(MonitorNumber);
        var monitorWidthValue = await sandBox.EvaluateActionInput<int>(MonitorWidth);
        var monitorHeightValue = await sandBox.EvaluateActionInput<int>(MonitorHeight);
        var monitorBitCountValue = await sandBox.EvaluateActionInput<int>(MonitorBitCount);
        var monitorFrequencyValue = await sandBox.EvaluateActionInput<int>(MonitorFrequency);

        _workstationService.SetScreenResolution(monitorNumberValue,
            monitorWidthValue, monitorHeightValue, monitorBitCountValue, monitorFrequencyValue);
    }
}