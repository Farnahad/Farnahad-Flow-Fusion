namespace FlowFusion.Service.FlowControl.FlowControl;

public class FlowControlService : IFlowControlService
{
    public async Task Wait(int duration)
    {
        await Task.Delay(TimeSpan.FromSeconds(duration));
    }
}