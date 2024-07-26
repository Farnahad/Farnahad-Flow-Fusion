using ManaErp.Core.Main.Mvvm;

namespace ManaErp.Service.Main.DateTime
{
    public interface IDateTimeService : IService
    {
        System.DateTime GetTodayDate();
        System.DateTime GetTodayDateTime();
    }
}