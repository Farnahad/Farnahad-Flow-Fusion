namespace FlowFusion.Service.Core.DateTime
{
    public interface IDateTimeService : IService
    {
        System.DateTime GetTodayDate();
        System.DateTime GetTodayDateTime();
    }
}