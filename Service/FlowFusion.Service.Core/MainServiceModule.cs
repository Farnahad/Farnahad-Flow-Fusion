using FlowFusion.Service.Core.DateTime;
using FlowFusion.Service.Core.Log;
using FlowFusion.Service.Core.MessageBox;
using FlowFusion.Service.Core.Navigation;
using FlowFusion.Service.Core.Session;
using FlowFusion.Service.Core.Text;
using FlowFusion.Service.Core.Timer;
using FlowFusion.Service.Core.Utility;
using FlowFusion.Service.Core.Window;
using Prism.Ioc;
using Prism.Modularity;

namespace FlowFusion.Service.Core
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