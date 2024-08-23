using FlowFusion.Service.WindowsServices.WindowsService.Base;

namespace FlowFusion.Service.WindowsServices.WindowsService;

public interface IWindowsServiceService
{
    bool IfService(IfService ifService, string serviceName);
    void PauseService(string serviceToPause);
    void ResumeService(string serviceToResume);
    void StartService(string serviceToStart);
    void StopService(string serviceToStop);
    void WaitForService(WaitForServiceTo waitForServiceTo, string serviceName,
        bool failWithTimeoutError, int duration);
}