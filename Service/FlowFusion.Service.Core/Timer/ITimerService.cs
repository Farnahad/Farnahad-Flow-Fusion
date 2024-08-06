namespace FlowFusion.Service.Core.Timer
{
    public interface ITimerService : IService
    {
        void SetConfig(Action action, int intervalSeconds);
        void Start();
        void Stop();
    }
}