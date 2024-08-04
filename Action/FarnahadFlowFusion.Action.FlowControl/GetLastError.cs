using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.FlowControl;

public class GetLastError : IAction
{
    public string Name => "Get last error";

    public Variable StoreInto { get; set; }
    public bool ClearError { get; set; }

    public GetLastError()
    {
        StoreInto = new Variable();
        ClearError = false;
    }

    public async Task Execute(SandBox sandBox)
    {
        StoreInto.Value = sandBox.Exception;

        if (ClearError)
            sandBox.Exception = null;

        sandBox.SetVariable(StoreInto);
        await Task.CompletedTask;
    }
}