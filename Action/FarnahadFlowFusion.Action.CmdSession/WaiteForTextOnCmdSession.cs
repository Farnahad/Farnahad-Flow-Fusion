using System.Diagnostics;
using System.Text.RegularExpressions;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.CmdSession;

public class WaiteForTextOnCmdSession : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Waite for text on CMD session";

    public ActionInput CmdSession { get; set; }
    public ActionInput TextToWait { get; set; }
    public bool IsRegularExpression { get; set; }
    public bool IgnoreCase { get; set; }
    public ActionInput Timeout { get; set; }

    public WaiteForTextOnCmdSession()
    {
        _cSharpService = new CSharpService();

        CmdSession = new ActionInput();
        TextToWait = new ActionInput();
        IsRegularExpression = false;
        IgnoreCase = true;
        Timeout = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var cmdSessionValue = await _cSharpService.EvaluateActionInput<Process>(sandBox, CmdSession);
        var textToWaitValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToWait);
        var timeoutValue = await _cSharpService.EvaluateActionInput<int>(sandBox, Timeout);

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