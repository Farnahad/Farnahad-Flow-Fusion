namespace FarnahadFlowFusion.Action.Main;

public interface IHandleErrorAction
{
    void OnError(Exception exception);
}