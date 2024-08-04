namespace FarnahadFlowFusion.Action.Main.Action;

public interface IHandleErrorAction
{
    void OnError(Exception exception);
}