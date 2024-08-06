using Prism.Ioc;

namespace FlowFusion.Module.Main.RibbonPage
{
    public partial class CrmRibbonPage
    {
        public CrmRibbonPage(IContainerExtension containerExtension)
        {
            InitializeComponent();
            DataContext = containerExtension.Resolve<CrmRibbonPageViewModel>();
        }
    }
}