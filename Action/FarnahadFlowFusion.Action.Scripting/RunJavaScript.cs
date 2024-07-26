using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Scripting;

public class RunJavaScript : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Run JavaScript";

    public ActionInput JavaScriptToRun { get; set; }
    public bool FailAfterTimeout { get; set; }
    public ActionInput Timeout { get; set; }
    public Variable JavascriptOutput { get; set; }

    public RunJavaScript()
    {
        _cSharpService = new CSharpService();

        JavaScriptToRun = new ActionInput();
        FailAfterTimeout = false;
        Timeout = new ActionInput();
        JavascriptOutput = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var javaScriptToRunValue = await _cSharpService.EvaluateActionInput<string>(sandBox, JavaScriptToRun);


        var engine = new Engine();
        var executeScriptTask = Task.Run(() => engine.Evaluate(javaScriptToRunValue).ToObject());

        if (FailAfterTimeout)
        {
            var timeoutValue = await _cSharpService.EvaluateActionInput<int>(sandBox, Timeout);
            if (await Task.WhenAny(executeScriptTask, Task.Delay(TimeSpan.FromSeconds(timeoutValue))) == executeScriptTask)
            {
                JavascriptOutput.Value = executeScriptTask.Result;
            }
            else
            {
                throw new TimeoutException("JavaScript execution timed out.");
            }
        }
        else
        {
            JavascriptOutput.Value = await executeScriptTask;
        }


        sandBox.Variables.Add(JavascriptOutput);
    }
}