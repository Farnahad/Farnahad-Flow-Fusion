using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Scripting;

public class RunJavaScript : IAction
{
    public string Name => "Run JavaScript";

    public ActionInput JavaScriptToRun { get; set; }
    public bool FailAfterTimeout { get; set; }
    public ActionInput Timeout { get; set; }
    public Variable JavascriptOutput { get; set; }

    public RunJavaScript()
    {
        JavaScriptToRun = new ActionInput();
        FailAfterTimeout = false;
        Timeout = new ActionInput();
        JavascriptOutput = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var javaScriptToRunValue = await sandBox.EvaluateActionInput<string>(JavaScriptToRun);

        var engine = new Engine();
        var executeScriptTask = Task.Run(() => engine.Evaluate(javaScriptToRunValue).ToObject());

        if (FailAfterTimeout)
        {
            var timeoutValue = await sandBox.EvaluateActionInput<int>(Timeout);
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

        sandBox.SetVariable(JavascriptOutput);
    }
}