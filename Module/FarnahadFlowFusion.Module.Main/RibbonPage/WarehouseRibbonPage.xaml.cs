using Prism.Ioc;

namespace ManaErp.Module.Main.RibbonPage
{
    public partial class WarehouseRibbonPage
    {
        public WarehouseRibbonPage(IContainerExtension containerExtension)
        {
            InitializeComponent();
            DataContext = containerExtension.Resolve<WarehouseRibbonPageViewModel>();
        }
    }
}