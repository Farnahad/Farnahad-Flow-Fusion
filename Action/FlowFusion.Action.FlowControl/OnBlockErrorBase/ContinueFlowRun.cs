namespace FlowFusion.Action.FlowControl.OnBlockErrorBase;

public enum ContinueFlowRun
{
    GoToBeggingOfBlock,
    GoToEndOfBlock,
    GoToLabel,
    GoToNextAction,
    RepeatAction
}