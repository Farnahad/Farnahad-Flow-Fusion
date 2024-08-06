using System.Diagnostics;
using System.Text.RegularExpressions;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.CmdSession;

public class WaiteForTextOnCmdSession : IAction
{
    public string Name => "Waite for text on CMD session";

    public ActionInput CmdSession { get; set; }
    public ActionInput TextToWait { get; set; }
    public bool IsRegularExpression { get; set; }
    public bool IgnoreCase { get; set; }
    public ActionInput Timeout { get; set; }

    public WaiteForTextOnCmdSession()
    {

        CmdSession = new ActionInput();
        TextToWait = new ActionInput();
        IsRegularExpression = false;
        IgnoreCase = true;
        Timeout = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var cmdSessionValue = await sandBox.EvaluateActionInput<Process>(CmdSession);
        var textToWaitValue = await sandBox.EvaluateActionInput<string>(TextToWait);
        var timeoutValue = await sandBox.EvaluateActionInput<int>(Timeout);

        var output = string.Empty;
        cmdSessionValue.OutputDataReceived += (sender, args) =>
        {
            if (string.IsNullOrEmpty(args.Data) == false)
            {
                output += args.Data + Environment.NewLine;

                if (IsRegularExpression)
                {
                    var options = IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;
                    var regex = new Regex(textToWaitValue, options);

                    if (regex.IsMatch(output))
                        Environment.Exit(0);
                }
                else
                {
                    if (IgnoreCase)
                    {
                        if (output.IndexOf(textToWaitValue, StringComparison.OrdinalIgnoreCase) >= 0)
                            Environment.Exit(0);
                    }
                    else
                    {
                        if (output.Contains(textToWaitValue))
                            Environment.Exit(0);
                    }
                }
            }
        };

        cmdSessionValue.BeginOutputReadLine();
        cmdSessionValue.BeginErrorReadLine();

        await cmdSessionValue.StandardInput.WriteLineAsync(textToWaitValue);
        await cmdSessionValue.StandardInput.WriteLineAsync("exit");
        await cmdSessionValue.WaitForExitAsync();
    }
}