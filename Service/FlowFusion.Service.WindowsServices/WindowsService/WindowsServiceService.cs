using System.ServiceProcess;
using System.Windows;
using FlowFusion.Service.WindowsServices.WindowsService.Base;

namespace FlowFusion.Service.WindowsServices.WindowsService;

public class WindowsServiceService : IWindowsServiceService
{
    public bool IfService(IfService ifService, string serviceName)
    {
        return ifService switch
        {
            Base.IfService.IsInstalled => IsInstalled(serviceName),
            Base.IfService.IsNotInstalled => IsInstalled(serviceName) == false,
            Base.IfService.IsPaused => GetStatus(serviceName) == ServiceControllerStatus.Paused,
            Base.IfService.IsRunning => GetStatus(serviceName) == ServiceControllerStatus.Running,
            Base.IfService.IsStopped => GetStatus(serviceName) == ServiceControllerStatus.Stopped,
            _ => false
        };
    }

    private bool IsInstalled(string serviceName)
    {
        var services = ServiceController.GetServices();
        foreach (var service in services)
        {
            if (service.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    private ServiceControllerStatus GetStatus(string serviceName)
    {
        using var serviceController = new ServiceController(serviceName);
        return serviceController.Status;
    }

    public void PauseService(string serviceToPause)
    {
        using var serviceController = new ServiceController(serviceToPause);
        if (serviceController.CanPauseAndContinue && serviceController.Status == ServiceControllerStatus.Running)
        {
            serviceController.Pause();
            serviceController.WaitForStatus(ServiceControllerStatus.Paused, TimeSpan.FromSeconds(30));
        }
    }

    public void ResumeService(string serviceToResume)
    {
        using var serviceController = new ServiceController(serviceToResume);
        if (serviceController.Status == ServiceControllerStatus.Paused)
        {
            serviceController.Continue();
            serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
        }
    }

    public void StartService(string serviceToStart)
    {
        using var serviceController = new ServiceController(serviceToStart);

        if (serviceController.Status == ServiceControllerStatus.Stopped)
        {
            serviceController.Start();
            serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
        }
    }

    public void StopService(string serviceToStop)
    {
        using var serviceController = new ServiceController(serviceToStop);
        if (serviceController.Status != ServiceControllerStatus.Stopped)
        {
            serviceController.Stop();
            serviceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
        }
    }

    public void WaitForService(WaitForServiceTo waitForServiceTo, string serviceName, bool failWithTimeoutError, int duration)
    {
        using var serviceController = new ServiceController(serviceName);

        if (failWithTimeoutError)
        {
            switch (waitForServiceTo)
            {
                case WaitForServiceTo.Pause:
                    serviceController.WaitForStatus(ServiceControllerStatus.Paused, TimeSpan.FromSeconds(duration));
                    break;
                case WaitForServiceTo.Start:
                    serviceController.WaitForStatus(ServiceControllerStatus.StartPending, TimeSpan.FromSeconds(duration));
                    break;
                case WaitForServiceTo.Stop:
                    serviceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(duration));
                    break;
            }
        }
        else
        {
            switch (waitForServiceTo)
            {
                case WaitForServiceTo.Pause:
                    serviceController.WaitForStatus(ServiceControllerStatus.Paused);
                    break;
                case WaitForServiceTo.Start:
                    serviceController.WaitForStatus(ServiceControllerStatus.StartPending);
                    break;
                case WaitForServiceTo.Stop:
                    serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                    break;
            }
        }
    }
}