using FarnahadFlowFusion.Action.FlowControl.StopFlowBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.FlowControl;

public class StopFlow : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Stop flow";

    public EndFlow EndFlow { get; set; }
    public ActionInput ErrorMessage { get; set; }

    public StopFlow()
    {
        _cSharpService = new CSharpService();

        EndFlow = EndFlow.Successfully;
        ErrorMessage = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var errorMessageValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ErrorMessage);
        switch (EndFlow)
        {
            case EndFlow.Successfully:
                sandBox.Exception = null;
                break;
            case EndFlow.WithErrorMessage:
                sandBox.Exception = new Exception(errorMessageValue);
                break;
        }
        sandBox.SandBoxStatus = SandBoxStatus.Stopping;
    }
}