using Prism.Ioc;

namespace ManaErp.Module.Main.RibbonPage
{
    public partial class PurchasingRibbonPage
    {
        public PurchasingRibbonPage(IContainerExtension containerExtension)
        {
            InitializeComponent();
            DataContext = containerExtension.Resolve<PurchasingRibbonPageViewModel>();
        }
    }
}