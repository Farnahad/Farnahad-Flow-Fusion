using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.WindowsServices.WindowsService;
using FlowFusion.Service.WindowsServices.WindowsService.Base;

namespace FlowFusion.Action.WindowsServices;

public class WaitForService(IWindowsServiceService windowsServiceService) : IAction
{
    public string Name => "Wait for service";

    public WaitForServiceTo WaitForServiceTo { get; set; } = WaitForServiceTo.Start;
    public ActionInput ServiceName { get; set; } = new();
    public bool FailWithTimeoutError { get; set; } = false;
    public ActionInput Duration { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var serviceNameValue = await sandBox.EvaluateActionInput<string>(ServiceName);
        var durationValue = await sandBox.EvaluateActionInput<int>(Duration);

        windowsServiceService.WaitForService(WaitForServiceTo, serviceNameValue, FailWithTimeoutError, durationValue);
    }
}