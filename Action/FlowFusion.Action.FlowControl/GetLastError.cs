using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.FlowControl;

public class GetLastError : GeneralAction
{
    public override string Name => "Get last error";

    public Variable StoreInto { get; set; }
    public bool ClearError { get; set; }

    public GetLastError()
    {
        StoreInto = new Variable();
        ClearError = false;
    }

    public override async Task Execute(SandBox sandBox)
    {
        StoreInto.Value = sandBox.Exception;

        if (ClearError)
            sandBox.Exception = null;

        sandBox.SetVariable(StoreInto);
        await Task.CompletedTask;
    }
}