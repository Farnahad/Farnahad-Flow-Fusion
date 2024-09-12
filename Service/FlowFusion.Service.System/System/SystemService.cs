using FlowFusion.Service.System.System.Base;
using System.Diagnostics;

namespace FlowFusion.Service.System.System;

public class SystemService : ISystemService
{
    public void DeleteWindowsEnvironmentVariable(string environmentVariableName,
        WindowsEnvironmentVariableType windowsEnvironmentVariableType)
    {
        throw new NotImplementedException();
    }

    public string GetWindowsEnvironmentVariable(string environmentVariableName, bool searchForVariableOnlyInScope, Scope scope)
    {
        throw new NotImplementedException();
    }

    public bool IfProcess(IfProcess ifProcess, string processName)
    {
        throw new NotImplementedException();
    }

    public async Task<(string, string)> Ping(string hostName, int timeOut)
    {
        var ping = new global::System.Net.NetworkInformation.Ping();
        var reply = await ping.SendPingAsync(hostName, timeOut * 1000);

        return (reply.Status.ToString(), reply.RoundtripTime.ToString());
    }

    public async Task<int> RunApplication(string applicationPath, string commandLineArguments, string workingFolder, WindowStyle windowStyle,
        AfterApplicationLunch afterApplicationLunch, int timeout)
    {
        var processWindowStyle = ProcessWindowStyle.Normal;

        switch (windowStyle)
        {
            case WindowStyle.Hidden:
                processWindowStyle = ProcessWindowStyle.Hidden;
                break;
            case WindowStyle.Maximized:
                processWindowStyle = ProcessWindowStyle.Maximized;
                break;
            case WindowStyle.Minimized:
                processWindowStyle = ProcessWindowStyle.Minimized;
                break;
            case WindowStyle.Normal:
                processWindowStyle = ProcessWindowStyle.Normal;
                break;
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = applicationPath,
            Arguments = commandLineArguments,
            WorkingDirectory = workingFolder,
            WindowStyle = processWindowStyle
        };

        using (var process = new Process())
        {
            process.StartInfo = startInfo;
            process.Start();

            switch (afterApplicationLunch)
            {
                case AfterApplicationLunch.ContinueImmediately:
                    break;

                case AfterApplicationLunch.WaitForApplicationToComplete:
                    if (timeout != 0)
                    {
                        if (await Task.Run(() => process.WaitForExit(TimeSpan.FromSeconds(timeout)))
                        {
                        }
                        else
                        {
                            process.Kill();
                            throw new TimeoutException("The application did not exit in the specified time.");
                        }
                    }
                    else
                    {
                        await process.WaitForExitAsync();
                    }
                    break;

                case AfterApplicationLunch.WaitForApplicationToLoad:
                    // Implement a mechanism to check if the application has fully loaded
                    // This is a placeholder and may need a more specific approach depending on the application
                    await Task.Delay(5000); // Simulate wait for the application to load
                    break;
            }

            return process.Id;
        }

    }

    public void SetWindowsEnvironmentVariable(string environmentVariableName, string newEnvironmentVariableValue,
        WindowsEnvironmentVariableType windowsEnvironmentVariableType)
    {
        switch (windowsEnvironmentVariableType)
        {
            case WindowsEnvironmentVariableType.System:
                Environment.SetEnvironmentVariable(environmentVariableName, null, EnvironmentVariableTarget.Machine);
                break;
            case WindowsEnvironmentVariableType.User:
                Environment.SetEnvironmentVariable(environmentVariableName, null, EnvironmentVariableTarget.User);
                break;
        }
    }

    public async Task TerminateProcess(SpecifyProcessBy specifyProcessBy, string processName, int processId)
    {
        switch (specifyProcessBy)
        {
            case SpecifyProcessBy.ProcessId:
                var processById = Process.GetProcessById(processId);
                processById.Kill();
                await processById.WaitForExitAsync();
                break;
            case SpecifyProcessBy.ProcessName:
                var processes = Process.GetProcessesByName(processName);
                foreach (var process in processes)
                {
                    process.Kill();
                    await process.WaitForExitAsync();
                }
                break;
        }
    }

    public async Task WaitForProcess(string processName, WaitForProcessTo waitForProcessTo,
        bool failWithTimeoutError, int duration)
    {
        var endTime = global::System.DateTime.UtcNow.AddSeconds(duration);

        var cancellationToken = new CancellationToken();

        bool waiteResult = false;

        while (global::System.DateTime.UtcNow < endTime)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var processExists = ProcessExists(processName);
            if ((waitForProcessTo == WaitForProcessTo.Start && processExists) ||
                (waitForProcessTo == WaitForProcessTo.Stop && !processExists))
            {
                waiteResult = true;
                break;
            }

            await Task.Delay(500, cancellationToken);
        }

        if (failWithTimeoutError && waiteResult == false)
        {
            throw new TimeoutException($"Timeout waiting for process '{processName}' to {waitCondition}.");
        }
    }

    private bool ProcessExists(string processName)
    {
        var processes = Process.GetProcessesByName(processName);
        return processes.Length > 0;
    }
}