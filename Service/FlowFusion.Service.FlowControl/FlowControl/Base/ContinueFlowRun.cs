namespace FlowFusion.Service.FlowControl.FlowControl.Base;

public enum ContinueFlowRun
{
    GoToBeggingOfBlock,
    GoToEndOfBlock,
    GoToLabel,
    GoToNextAction,
    RepeatAction
}