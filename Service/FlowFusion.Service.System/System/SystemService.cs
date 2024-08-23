using FlowFusion.Service.System.System.Base;

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

    public (string, string) Ping(string hostName, int timeOut)
    {
        throw new NotImplementedException();
    }

    public int RunApplication(string applicationPath, string commandLineArguments, string workingFolder, WindowStyle windowStyle,
        AfterApplicationLunch afterApplicationLunch, int timeout)
    {
        throw new NotImplementedException();
    }

    public void SetWindowsEnvironmentVariable(string environmentVariableName, string newEnvironmentVariableValue,
        WindowsEnvironmentVariableType windowsEnvironmentVariableType)
    {
        throw new NotImplementedException();
    }

    public void TerminateProcess(SpecifyProcessBy specifyProcessBy, string processName, int processId)
    {
        throw new NotImplementedException();
    }

    public void WaitForProcess(string processName, WaitForProcessTo waitForProcessTo, bool failWithTimeoutError, int duration)
    {
        throw new NotImplementedException();
    }
}