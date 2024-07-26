using ManaErp.Service.Main.DateTime;
using ManaErp.Service.Main.Log;
using ManaErp.Service.Main.MessageBox;
using ManaErp.Service.Main.Navigation;
using ManaErp.Service.Main.Session;
using ManaErp.Service.Main.Text;
using ManaErp.Service.Main.Timer;
using ManaErp.Service.Main.Utility;
using ManaErp.Service.Main.Window;
using Prism.Ioc;
using Prism.Modularity;

namespace ManaErp.Service.Main
{
    public class MainServiceModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDateTimeService, DateTimeService>();
            //containerRegistry.Register(typeof(IDatabaseService<>), typeof(DatabaseService<>));
            containerRegistry.Register<ILogService, LogService>();
            containerRegistry.Register<IMessageBoxService, MessageBoxService>();
            containerRegistry.Register<INavigationService, NavigationService>();
            containerRegistry.Register<ISessionService, SessionService>();
            containerRegistry.Register<ITextService, TextService>();
            containerRegistry.Register<ITimerService, TimerService>();
            containerRegistry.Register<IUtilityService, UtilityService>();
            containerRegistry.Register<IWindowService, WindowService>();
        }
    }
}