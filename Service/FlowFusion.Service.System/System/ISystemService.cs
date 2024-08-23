using FlowFusion.Service.System.System.Base;

namespace FlowFusion.Service.System.System;

public interface ISystemService
{
    void DeleteWindowsEnvironmentVariable(string environmentVariableName,
        WindowsEnvironmentVariableType windowsEnvironmentVariableType);
    string GetWindowsEnvironmentVariable(string environmentVariableName,
        bool searchForVariableOnlyInScope, Scope scope);
    bool IfProcess(Service.System.System.Base.IfProcess ifProcess, string processName);
    (string, string) Ping(string hostName, int timeOut);
    int RunApplication(string applicationPath, string commandLineArguments, string workingFolder,
        WindowStyle windowStyle, AfterApplicationLunch afterApplicationLunch, int timeout);
    void SetWindowsEnvironmentVariable(string environmentVariableName, string newEnvironmentVariableValue,
        WindowsEnvironmentVariableType windowsEnvironmentVariableType);
    void TerminateProcess(SpecifyProcessBy specifyProcessBy, string processName, int processId);
    void WaitForProcess(string processName, WaitForProcessTo waitForProcessTo,
        bool failWithTimeoutError, int duration);
}