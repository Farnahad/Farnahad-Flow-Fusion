namespace FlowFusion.Service.Core.Log
{
    public class LogService : ILogService
    {
        //private NLog.Logger _logger;

        public LogService()
        {
            //_logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public void Info(string log)
        {
            //_logger.Info(log);
        }

        public void Warn(string log)
        {
            //_logger.Warn(log);
        }

        public void Error(string log)
        {
            //_logger.Error(log);
        }

        public void Dispose()
        {
            //_logger = null;
        }
    }
}