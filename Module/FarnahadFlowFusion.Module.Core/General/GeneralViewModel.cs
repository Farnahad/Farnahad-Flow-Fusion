﻿using ManaErp.Core.Main.Mvvm;
using ManaErp.Service.Core.Container;
using ManaErp.Service.Main.MessageBox;
using ManaErp.Service.Main.Navigation;
using ManaErp.Service.Main.Window;
using Prism.Events;

namespace ManaErp.Module.Core.General
{
    public class GeneralViewModel : ViewModel
    {
        protected GeneralViewModel(IContainerService containerService)
        {
            ContainerService = containerService;
        }


        #region Services

        protected IContainerService ContainerService { get; set; }

        private IEventAggregator _eventAggregator;
        protected IEventAggregator EventAggregator
        {
            get
            {
                if (_eventAggregator == null)
                    _eventAggregator = ContainerService.Resolve<IEventAggregator>();

                return _eventAggregator;
            }
        }

        private IMessageBoxService _messageBoxService;
        protected IMessageBoxService MessageBoxService
        {
            get
            {
                if (_messageBoxService == null)
                    _messageBoxService = ContainerService.Resolve<IMessageBoxService>();

                return _messageBoxService;
            }
        }

        private INavigationService _navigationService;
        protected INavigationService NavigationService
        {
            get
            {
                if (_navigationService == null)
                    _navigationService = ContainerService.Resolve<INavigationService>();

                return _navigationService;
            }
        }

        private IWindowService _windowService;
        protected IWindowService WindowService
        {
            get
            {
                if (_windowService == null)
                    _windowService = ContainerService.Resolve<IWindowService>();

                return _windowService;
            }
        }

        #endregion


        public override void Dispose()
        {
            ContainerService?.Dispose();
            _eventAggregator = null;
            MessageBoxService?.Dispose();
            NavigationService?.Dispose();
            WindowService?.Dispose();
        }
    }
}