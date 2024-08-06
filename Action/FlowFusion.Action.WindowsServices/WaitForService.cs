using System.ServiceProcess;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.WindowsServices.WaitForServiceBase;
using FlowFusion.Service.WindowsServices.WindowsService;

namespace FlowFusion.Action.WindowsServices;

public class WaitForService : IAction
{
    private readonly WindowsServiceService _windowsServiceService;

    public string Name => "Wait for service";

    public WaitForServiceTo WaitForServiceTo { get; set; }
    public ActionInput ServiceName { get; set; }
    public bool FailWithTimeoutError { get; set; }
    public ActionInput Duration { get; set; }

    public WaitForService()
    {
        _windowsServiceService = new WindowsServiceService();

        WaitForServiceTo = WaitForServiceTo.Start;
        ServiceName = new ActionInput();
        FailWithTimeoutError = false;
        Duration = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var serviceNameValue = await sandBox.EvaluateActionInput<string>(ServiceName);

        if (FailWithTimeoutError)
        {
            var durationValue = await sandBox.EvaluateActionInput<int>(Duration);

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