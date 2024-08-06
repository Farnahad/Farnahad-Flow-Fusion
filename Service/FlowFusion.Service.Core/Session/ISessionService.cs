namespace FlowFusion.Service.Core.Session
{
    public interface ISessionService : IService
    {
        ISessionService Instance { get; }
        void RunOnUiThread(Action action);
        void ExitApplication();
    }
}