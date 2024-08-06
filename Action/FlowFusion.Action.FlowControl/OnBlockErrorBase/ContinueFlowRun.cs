namespace FarnahadFlowFusion.Action.FlowControl.OnBlockErrorBase;

public enum ContinueFlowRun
{
    GoToBeggingOfBlock,
    GoToEndOfBlock,
    GoToLabel,
    GoToNextAction,
    RepeatAction
}