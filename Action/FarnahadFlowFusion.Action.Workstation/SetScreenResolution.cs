using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Workstation;

public class SetScreenResolution : IAction
{
    private readonly CSharpService _cSharpService;
    private readonly WorkstationService _workstationService;

    public string Name => "Set screen resolution";

    public ActionInput MonitorNumber { get; set; }
    public ActionInput MonitorWidth { get; set; }
    public ActionInput MonitorHeight { get; set; }
    public ActionInput MonitorBitCount { get; set; }
    public ActionInput MonitorFrequency { get; set; }

    public SetScreenResolution()
    {
        _cSharpService = new CSharpService();
        _workstationService = new WorkstationService();

        MonitorNumber = new ActionInput();
        MonitorWidth = new ActionInput();
        MonitorHeight = new ActionInput();
        MonitorBitCount = new ActionInput();
        MonitorFrequency = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var monitorNumberValue = await _cSharpService.EvaluateActionInput<int>(sandBox, MonitorNumber);
        var monitorWidthValue = await _cSharpService.EvaluateActionInput<int>(sandBox, MonitorWidth);
        var monitorHeightValue = await _cSharpService.EvaluateActionInput<int>(sandBox, MonitorHeight);
        var monitorBitCountValue = await _cSharpService.EvaluateActionInput<int>(sandBox, MonitorBitCount);
        var monitorFrequencyValue = await _cSharpService.EvaluateActionInput<int>(sandBox, MonitorFrequency);

        _workstationService.SetScreenResolution(monitorNumberValue,
            monitorWidthValue, monitorHeightValue, monitorBitCountValue, monitorFrequencyValue);
    }
}