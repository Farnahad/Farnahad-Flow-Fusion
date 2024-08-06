using System.ServiceProcess;

namespace FarnahadFlowFusion.Service.WindowsServices.WindowsService;

public class WindowsServiceService
{
    public bool IsInstalled(string serviceName)
    {
        var services = ServiceController.GetServices();
        foreach (var service in services)
        {
            if (service.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    public ServiceControllerStatus GetStatus(string serviceName)
    {
        using var serviceController = new ServiceController(serviceName);
        return serviceController.Status;
    }

    public void Start(string serviceName)
    {
        using var serviceController = new ServiceController(serviceName);

        if (serviceController.Status == ServiceControllerStatus.Stopped)
        {
            serviceController.Start();
            serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
        }
    }

    public void Stop(string serviceName)
    {
        using var serviceController = new ServiceController(serviceName);
        if (serviceController.Status != ServiceControllerStatus.Stopped)
        {
            serviceController.Stop();
            serviceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
        }
    }

    public void Resume(string serviceName)
    {
        using var serviceController = new ServiceController(serviceName);
        if (serviceController.Status == ServiceControllerStatus.Paused)
        {
            serviceController.Continue();
            serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
        }
    }

    public void Pause(string serviceName)
    {
        using var serviceController = new ServiceController(serviceName);
        if (serviceController.CanPauseAndContinue && serviceController.Status == ServiceControllerStatus.Running)
        {
            serviceController.Pause();
            serviceController.WaitForStatus(ServiceControllerStatus.Paused, TimeSpan.FromSeconds(30));
        }
    }

    public void WaitForStatus(string serviceName, ServiceControllerStatus serviceControllerStatus)
    {
        using var serviceController = new ServiceController(serviceName);
        serviceController.WaitForStatus(serviceControllerStatus);
    }

    public void WaitForStatus(string serviceName, ServiceControllerStatus serviceControllerStatus, int seconds)
    {
        using var serviceController = new ServiceController(serviceName);
        serviceController.WaitForStatus(serviceControllerStatus, TimeSpan.FromSeconds(seconds));
    }
}