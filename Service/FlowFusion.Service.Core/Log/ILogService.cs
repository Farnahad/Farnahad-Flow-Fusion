namespace FlowFusion.Service.Core.Log
{
    public interface ILogService : IService
    {
        void Info(string log);
        void Warn(string log);
        void Error(string log);
    }
}