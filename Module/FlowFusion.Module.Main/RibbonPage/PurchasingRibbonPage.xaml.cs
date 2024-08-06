using Prism.Ioc;

namespace FlowFusion.Module.Main.RibbonPage
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