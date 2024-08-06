using ManaErp.Module.Main.RibbonPage;
using ManaErp.Module.Main.View.StatusBar;
using Prism.Ioc;
using Prism.Modularity;

namespace ManaErp.Module.Main
{
    public class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var navigationService = containerProvider.Resolve<INavigationService>();

            navigationService.NavigateRibbon<ApplicationRibbonPage>();
            navigationService.NavigateRibbon<AssetRibbonPage>();
            navigationService.NavigateRibbon<BaseRibbonPage>();
            navigationService.NavigateRibbon<CrmRibbonPage>();
            navigationService.NavigateRibbon<EntityRibbonPage>();
            navigationService.NavigateRibbon<FinanceRibbonPage>();
            navigationService.NavigateRibbon<GeneralRibbonPage>();
            navigationService.NavigateRibbon<HumanResourcesRibbonPage>();
            navigationService.NavigateRibbon<ManufacturingRibbonPage>();
            navigationService.NavigateRibbon<PurchasingRibbonPage>();
            navigationService.NavigateRibbon<ResourceRibbonPage>();
            navigationService.NavigateRibbon<SalesRibbonPage>();
            navigationService.NavigateRibbon<WarehouseRibbonPage>();

            navigationService.NavigateStatusBar<StatusBarView>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<StatusBarView>("StatusBarView");
        }
    }
}