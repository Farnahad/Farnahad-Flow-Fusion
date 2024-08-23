using System.Diagnostics;

namespace FlowFusion.Service.CmdSession.CmdSession;

public interface ICmdSessionService
{
    void CloseCmdSession(Process cmdSession);
    Task<Process> OpenCmdSession(string workingDirectory, bool changeCodePage);
    Task<string> ReadFromCmdSession(Process cmdSession, bool separateOutputFromError);
    Task WaiteForTextOnCmdSession(Process cmdSession, string textToWait,
        bool isRegularExpression, bool ignoreCase, int timeout);
    Task WriteToCmdSession(Process cmdSession, string command, bool sendEnterAfterCommand);
}