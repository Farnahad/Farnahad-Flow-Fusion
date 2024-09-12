namespace FlowFusion.Action.Main.Action;

public interface IExceptionAwareAction : IAction
{
    Exception Exception { get; }
}