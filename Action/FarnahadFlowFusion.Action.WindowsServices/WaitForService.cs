using System.ServiceProcess;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.WindowsServices.WaitForServiceBase;

namespace FarnahadFlowFusion.Action.WindowsServices;

public class WaitForService : IAction
{
    private readonly CSharpService _cSharpService;
    private readonly WindowsServiceService _windowsServiceService;

    public string Name => "Wait for service";

    public WaitForServiceTo WaitForServiceTo { get; set; }
    public ActionInput ServiceName { get; set; }
    public bool FailWithTimeoutError { get; set; }
    public ActionInput Duration { get; set; }

    public WaitForService()
    {
        _cSharpService = new CSharpService();
        _windowsServiceService = new WindowsServiceService();

        WaitForServiceTo = WaitForServiceTo.Start;
        ServiceName = new ActionInput();
        FailWithTimeoutError = false;
        Duration = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var serviceNameValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ServiceName);

        if (FailWithTimeoutError)
        {
            var durationValue = await _cSharpService.EvaluateActionInput<int>(sandBox, Duration);

            switch (WaitForServiceTo)
            {
                case WaitForServiceTo.Pause:
                    _windowsServiceService.WaitForStatus(serviceNameValue, ServiceControllerStatus.Paused, durationValue);
                    break;
                case WaitForServiceTo.Start:
                    _windowsServiceService.WaitForStatus(serviceNameValue, ServiceControllerStatus.StartPending, durationValue);
                    break;
                case WaitForServiceTo.Stop:
                    _windowsServiceService.WaitForStatus(serviceNameValue, ServiceControllerStatus.Stopped, durationValue);
                    break;
            }
        }
        else
        {
            switch (WaitForServiceTo)
            {
                case WaitForServiceTo.Pause:
                    _windowsServiceService.WaitForStatus(serviceNameValue, ServiceControllerStatus.Paused);
                    break;
                case WaitForServiceTo.Start:
                    _windowsServiceService.WaitForStatus(serviceNameValue, ServiceControllerStatus.StartPending);
                    break;
                case WaitForServiceTo.Stop:
                    _windowsServiceService.WaitForStatus(serviceNameValue, ServiceControllerStatus.Stopped);
                    break;
            }
        }
    }
}