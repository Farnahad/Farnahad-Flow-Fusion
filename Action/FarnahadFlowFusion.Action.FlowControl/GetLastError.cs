using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.FlowControl;

public class GetLastError : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Get last error";

    public Variable StoreInto { get; set; }
    public bool ClearError { get; set; }

    public GetLastError()
    {
        _cSharpService = new CSharpService();

        StoreInto = new Variable();
        ClearError = false;
    }

    public async Task Execute(SandBox sandBox)
    {
        StoreInto.Value = sandBox.Exception;

        if (ClearError)
            sandBox.Exception = null;

        sandBox.Variables.Add(StoreInto);
        await Task.CompletedTask;
    }
}