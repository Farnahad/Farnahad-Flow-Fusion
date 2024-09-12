namespace FlowFusion.Service.FlowControl.FlowControl;

public interface IFlowControlService
{
    Task Wait(int duration);
}