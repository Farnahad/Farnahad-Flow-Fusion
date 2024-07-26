using System;
using System.Reflection;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using ManaErp.Core.Control.DockLayout;
using ManaErp.Core.Control.Mvvm;
using ManaErp.Core.Control.Ribbon;
using ManaErp.Core.Control.Window;
using ManaErp.Module.Application;
using ManaErp.Module.Asset;
using ManaErp.Module.Base;
using ManaErp.Module.Core;
using ManaErp.Module.Crm;
using ManaErp.Module.Entity;
using ManaErp.Module.Finance;
using ManaErp.Module.General;
using ManaErp.Module.HumanResources;
using ManaErp.Module.Manufacturing;
using ManaErp.Module.Purchasing;
using ManaErp.Module.Resource;
using ManaErp.Module.Sales;
using ManaErp.Module.Warehouse;
using ManaErp.Service.Application;
using ManaErp.Service.Asset;
using ManaErp.Service.Base;
using ManaErp.Service.Core;
using ManaErp.Service.Crm;
using ManaErp.Service.Entity;
using ManaErp.Service.Finance;
using ManaErp.Service.General;
using ManaErp.Service.HumanResources;
using ManaErp.Service.Main;
using ManaErp.Service.Manufacturing;
using ManaErp.Service.Purchasing;
using ManaErp.Service.Resource;
using ManaErp.Service.Sales;
using ManaErp.Service.Warehouse;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace ManaErp.Module.Main
{
    public partial class App
    {
        static App()
        {
            SetTheme();
            ShowSplashScreen();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
            return Container.Resolve<MainMeDxRibbonWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialogWindow<MeDxWindow>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(MeRibbonControl),
                Container.Resolve<MeRibbonControlRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(MeAutoHideGroup),
                Container.Resolve<MeAutoHideGroupRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(MeDocumentGroup),
                Container.Resolve<MeDocumentGroupRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(MeDockLayoutGroup),
                Container.Resolve<MeDockLayoutGroupRegionAdapter>());
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            #region Services

            moduleCatalog.AddModule<ApplicationServiceModule>();
            moduleCatalog.AddModule<AssetServiceModule>();
            moduleCatalog.AddModule<BaseServiceModule>();
            moduleCatalog.AddModule<CoreServiceModule>();
            moduleCatalog.AddModule<CrmServiceModule>();
            moduleCatalog.AddModule<EntityServiceModule>();
            moduleCatalog.AddModule<FinanceServiceModule>();
            moduleCatalog.AddModule<GeneralServiceModule>();
            moduleCatalog.AddModule<HumanResourcesServiceModule>();
            moduleCatalog.AddModule<MainServiceModule>();
            moduleCatalog.AddModule<ManufacturingServiceModule>();
            moduleCatalog.AddModule<PurchasingServiceModule>();
            moduleCatalog.AddModule<ResourceServiceModule>();
            moduleCatalog.AddModule<SalesServiceModule>();
            moduleCatalog.AddModule<WarehouseServiceModule>();

            #endregion


            #region Modules

            moduleCatalog.AddModule<ApplicationModule>();
            moduleCatalog.AddModule<AssetModule>();
            moduleCatalog.AddModule<BaseModule>();
            moduleCatalog.AddModule<CoreModule>();
            moduleCatalog.AddModule<CrmModule>();
            moduleCatalog.AddModule<EntityModule>();
            moduleCatalog.AddModule<FinanceModule>();
            moduleCatalog.AddModule<GeneralModule>();
            moduleCatalog.AddModule<HumanResourcesModule>();
            moduleCatalog.AddModule<MainModule>();
            moduleCatalog.AddModule<ManufacturingModule>();
            moduleCatalog.AddModule<PurchasingModule>();
            moduleCatalog.AddModule<ResourceModule>();
            moduleCatalog.AddModule<SalesModule>();
            moduleCatalog.AddModule<WarehouseModule>();

            #endregion
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = $"{viewName}Model, {viewAssemblyName}";

                return Type.GetType(viewModelName);
            });
        }

        private static void SetTheme()
        {
             ApplicationThemeHelper.ApplicationThemeName = "Office2013DarkGray";
        }

        private static void ShowSplashScreen()
        {
            var splashScreenViewModel = new DXSplashScreenViewModel
            {
                Title = "Mana ERP",
                IsIndeterminate = true,
                Subtitle = "Powered by Farnahad",
                Copyright = "Copyright © " + DateTime.Now.Year + " Farnahad Company.\nAll rights reserved."
            };

            SplashScreenManager.Create(() => new ManaErpSplashScreen
            {
            }, splashScreenViewModel).ShowOnStartup();
        }
    }
}