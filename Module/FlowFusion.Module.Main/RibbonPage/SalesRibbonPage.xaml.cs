﻿using Prism.Ioc;

namespace ManaErp.Module.Main.RibbonPage
{
    public partial class SalesRibbonPage
    {
        public SalesRibbonPage(IContainerExtension containerExtension)
        {
            InitializeComponent();
            DataContext = containerExtension.Resolve<SalesRibbonPageViewModel>();
        }
    }
}