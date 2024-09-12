using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.FlowControl.FlowControl.Base;

namespace FlowFusion.Action.FlowControl;

public class OnBlockError : GeneralAction
{
    public override string Name => "On block error";

    // ReSharper disable once InconsistentNaming
    public string _Name { get; set; }
    public List<SetVariable> SetVariables { get; set; }
    public List<Service.FlowControl.FlowControl.Base.RunSubflow> RunSubflows { get; set; }
    public OnBlockErrorType Type { get; set; }
    public ContinueFlowRun ContinueFlowRun { get; set; }
    public string Label { get; set; }
    public bool CaptureOnExpectedLogicErrors { get; set; }

    public OnBlockError()
    {
        _Name = "";
        SetVariables = new List<SetVariable>();
        RunSubflows = new List<Service.FlowControl.FlowControl.Base.RunSubflow>();
        Type = OnBlockErrorType.ThrowError;
        ContinueFlowRun = ContinueFlowRun.GoToEndOfBlock;
        Label = "";
        CaptureOnExpectedLogicErrors = false;
    }

    public override async Task Execute(SandBox sandBox)
    {
        foreach (var setVariable in SetVariables.OrderBy(item => item.Order))
            setVariable.Variable.Value = await sandBox.EvaluateActionInput<object>(setVariable.Value);

        foreach (var runSubflow in RunSubflows.OrderBy(item => item.Order))
            await sandBox.Run(runSubflow.WorkFlow);


        if (Type == OnBlockErrorType.ContinueFlowRun)
        {
            switch (ContinueFlowRun)
            {
                case ContinueFlowRun.GoToBeggingOfBlock:
                    sandBox.GoToBeggingOfBlock();
                    break;
                case ContinueFlowRun.GoToEndOfBlock:
                    sandBox.GoToEndOfBlock();
                    break;
                case ContinueFlowRun.GoToLabel:
                    sandBox.GoToLabel(Label);
                    break;
                case ContinueFlowRun.GoToNextAction:
                    sandBox.GoToNextAction();
                    break;
                case ContinueFlowRun.RepeatAction:
                    sandBox.RepeatAction();
                    break;
            }
        }
        else if (Type == OnBlockErrorType.ThrowError)
        {
            if (CaptureOnExpectedLogicErrors)
                sandBox.ThrowError(sandBox.Exception);
        }
    }
}