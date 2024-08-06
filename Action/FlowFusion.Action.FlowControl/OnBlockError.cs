using FarnahadFlowFusion.Action.FlowControl.OnBlockErrorBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using Type = FarnahadFlowFusion.Action.FlowControl.OnBlockErrorBase.Type;

namespace FarnahadFlowFusion.Action.FlowControl;

public class OnBlockError : IAction
{
    public string Name => "On block error";

    public string _Name { get; set; }
    public List<SetVariable> SetVariables { get; set; }
    public List<OnBlockErrorBase.RunSubflow> RunSubflows { get; set; }
    public Type Type { get; set; }
    public ContinueFlowRun ContinueFlowRun { get; set; }
    public string Label { get; set; }
    public bool CaptureOnExpectedLogicErrors { get; set; }

    public OnBlockError()
    {
        _Name = "";
        SetVariables = new List<SetVariable>();
        RunSubflows = new List<OnBlockErrorBase.RunSubflow>();
        Type = Type.ThrowError;
        ContinueFlowRun = ContinueFlowRun.GoToEndOfBlock;
        Label = "";
        CaptureOnExpectedLogicErrors = false;
    }

    public async Task Execute(SandBox sandBox)
    {
        foreach (var setVariable in SetVariables.OrderBy(item => item.Order))
            setVariable.Variable.Value = await sandBox.EvaluateActionInput<object>(setVariable.Value);

        foreach (var runSubflow in RunSubflows.OrderBy(item => item.Order))
            await new SandBox(runSubflow.WorkFlow).Run();


        if (Type == Type.ContinueFlowRun)
        {
            switch (ContinueFlowRun)
            {
                case ContinueFlowRun.GoToBeggingOfBlock:
                    break;
                case ContinueFlowRun.GoToEndOfBlock:
                    break;
                case ContinueFlowRun.GoToLabel:
                    break;
                case ContinueFlowRun.GoToNextAction:
                    break;
                case ContinueFlowRun.RepeatAction:
                    break;
            }
        }
        else if (Type == Type.ThrowError)
        {
            if (CaptureOnExpectedLogicErrors)
                throw sandBox.Exception;
        }
    }
}