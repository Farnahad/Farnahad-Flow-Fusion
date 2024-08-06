using Prism.Ioc;

namespace FlowFusion.Module.Main.RibbonPage
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