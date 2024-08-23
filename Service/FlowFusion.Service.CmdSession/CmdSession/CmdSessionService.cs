using System.Diagnostics;
using System.Text.RegularExpressions;

namespace FlowFusion.Service.CmdSession.CmdSession;

public class CmdSessionService : ICmdSessionService
{
    public void CloseCmdSession(Process cmdSession)
    {
        cmdSession.Kill(true);
    }

    public async Task<Process> OpenCmdSession(string workingDirectory, bool changeCodePage)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            WorkingDirectory = workingDirectory,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = false
        };

        var process = Process.Start(startInfo);
        if (changeCodePage)
        {
            // ReSharper disable once PossibleNullReferenceException
            await process.StandardInput.WriteLineAsync("chcp 65001"); // Change code page to UTF-8
        }

        // ReSharper disable once PossibleNullReferenceException
        process.OutputDataReceived += (sender, args) =>
        {
            if (string.IsNullOrEmpty(args.Data) == false)
            {
            }
        };

        process.BeginOutputReadLine();
        await process.WaitForExitAsync();

        return process;
    }

    public async Task<string> ReadFromCmdSession(Process cmdSession, bool separateOutputFromError)
    {
        string result = null;

        if (separateOutputFromError)
        {
            cmdSession.OutputDataReceived += (sender, args) =>
            {
                if (string.IsNullOrEmpty(args.Data) == false)
                    result = args.Data;
            };

            cmdSession.ErrorDataReceived += (sender, args) =>
            {
                if (string.IsNullOrEmpty(args.Data) == false)
                    result = args.Data;
            };

            cmdSession.BeginOutputReadLine();
            cmdSession.BeginErrorReadLine();
        }
        else
        {
            cmdSession.OutputDataReceived += (sender, args) =>
            {
                if (string.IsNullOrEmpty(args.Data) == false)
                    result = args.Data;
            };

            cmdSession.BeginOutputReadLine();
        }

        await cmdSession.WaitForExitAsync();

        return result;
    }

    public async Task WaiteForTextOnCmdSession(Process cmdSession, string textToWait,
        bool isRegularExpression, bool ignoreCase, int timeout)
    {
        var output = string.Empty;
        cmdSession.OutputDataReceived += (sender, args) =>
        {
            if (string.IsNullOrEmpty(args.Data) == false)
            {
                output += args.Data + Environment.NewLine;

                if (isRegularExpression)
                {
                    var options = ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;
                    var regex = new Regex(textToWait, options);

                    if (regex.IsMatch(output))
                        Environment.Exit(0);
                }
                else
                {
                    if (ignoreCase)
                    {
                        if (output.IndexOf(textToWait, StringComparison.OrdinalIgnoreCase) >= 0)
                            Environment.Exit(0);
                    }
                    else
                    {
                        if (output.Contains(textToWait))
                            Environment.Exit(0);
                    }
                }
            }
        };

        cmdSession.BeginOutputReadLine();
        cmdSession.BeginErrorReadLine();

        await cmdSession.StandardInput.WriteLineAsync(textToWait);
        await cmdSession.StandardInput.WriteLineAsync("exit");
        await cmdSession.WaitForExitAsync();
    }

    public async Task WriteToCmdSession(Process cmdSession, string command, bool sendEnterAfterCommand)
    {
        await cmdSession.StandardInput.WriteLineAsync(command);

        if (sendEnterAfterCommand)
            await cmdSession.StandardInput.WriteLineAsync();

        await cmdSession.StandardInput.WriteLineAsync("exit");
        await cmdSession.WaitForExitAsync();
    }
}