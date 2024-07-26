using System;
using ManaErp.Core.Control.DockLayout;
using ManaErp.Core.Control.Ribbon;
using ManaErp.Core.Main.Mvvm;
using ManaErp.Service.Core.Container;
using Prism.Regions;

namespace ManaErp.Service.Main.Navigation
{
    public class NavigationService : INavigationService
    {
        private IRegionManager _regionManager;
        private readonly IContainerService _containerService;

        public NavigationService(IRegionManager regionManager,
            IContainerService containerService)
        {
            _regionManager = regionManager;
            _containerService = containerService;
        }

        public void NavigateRibbon<T>() where T : MeRibbonPage
        {
            Navigate<T>("Ribbon");
        }

        public void NavigateDock<T>(MeDockPotion meDockPotion,
            MeDockType meDockType = MeDockType.Visible) where T : View
        {
            NavigateToDock<T>(meDockPotion, meDockType);
        }

        public void NavigateDock<T>(MeDockPotion meDockPotion,
            params Tuple<string, object>[] parameters) where T : View
        {
            NavigateToDock<T>(meDockPotion, MeDockType.Visible, parameters);
        }

        public void NavigateStatusBar<T>() where T : View
        {
            Navigate<T>("StatusBar");
        }

        private void Navigate<T>(string regionName)
        {
            var region = _regionManager.Regions[regionName];

            //var sp = new Stopwatch();
            //sp.Start();
            var view = _containerService.Resolve<T>();
            //sp.Stop();
            //System.Windows.MessageBox.Show(sp.ElapsedMilliseconds.ToString());

            region.Add(view);
            region.Activate(view);
        }

        private void NavigateToDock<T>(MeDockPotion meDockPotion, MeDockType meDockType,
            params Tuple<string, object>[] parameters) where T : View
        {
            var viewName = typeof(T).Name;
            _containerService.RegisterNavigation<T>();

            var navigationParameters = new NavigationParameters();
            foreach (var parameter in parameters)
                navigationParameters.Add(parameter.Item1, parameter.Item2);

            if (meDockType == MeDockType.Visible)
            {
                switch (meDockPotion)
                {
                    case MeDockPotion.Bottom:
                        _regionManager.RequestNavigate("BottomLayoutGroup", viewName, navigationParameters);
                        break;
                    case MeDockPotion.Document:
                        _regionManager.RequestNavigate("DocumentLayoutGroup", viewName, navigationParameters);
                        break;
                    case MeDockPotion.Left:
                        _regionManager.RequestNavigate("LeftLayoutGroup", viewName, navigationParameters);
                        break;
                    case MeDockPotion.Right:
                        _regionManager.RequestNavigate("RightLayoutGroup", viewName, navigationParameters);
                        break;
                    case MeDockPotion.Top:
                        _regionManager.RequestNavigate("TopLayoutGroup", viewName, navigationParameters);
                        break;
                }
            }
            else if (meDockType == MeDockType.Hide)
            {
                switch (meDockPotion)
                {
                    case MeDockPotion.Bottom:
                        _regionManager.RequestNavigate("BottomAutoHideGroup", viewName, navigationParameters);
                        break;
                    case MeDockPotion.Document:
                        break;
                    case MeDockPotion.Left:
                        _regionManager.RequestNavigate("LeftAutoHideGroup", viewName, navigationParameters);
                        break;
                    case MeDockPotion.Right:
                        _regionManager.RequestNavigate("RightAutoHideGroup", viewName, navigationParameters);
                        break;
                    case MeDockPotion.Top:
                        _regionManager.RequestNavigate("TopAutoHideGroup", viewName, navigationParameters);
                        break;
                }
            }
        }

        public void Dispose()
        {
            _regionManager = null;
            _containerService?.Dispose();
        }
    }
}