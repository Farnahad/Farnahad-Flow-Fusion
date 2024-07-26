using System;
using ManaErp.Service.Core.Container;

namespace ManaErp.Service.Main.Session
{
    public class SessionService : ISessionService
    {
        private ISessionService _instance;
        private readonly IContainerService _containerService;

        public ISessionService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = _containerService.Resolve<ISessionService>();

                return _instance;
            }
            private set => _instance = value;
        }

        public SessionService(IContainerService containerService)
        {
            _containerService = containerService;
            Instance = this;
        }

        public void RunOnUiThread(Action action)
        {
            if (System.Windows.Application.Current.Dispatcher != null)
                System.Windows.Application.Current.Dispatcher.Invoke(action);
        }

        public void ExitApplication()
        {
            System.Windows.Application.Current.Shutdown();
        }

        public void Dispose()
        {
            _instance?.Dispose();
            _containerService?.Dispose();
        }
    }
}