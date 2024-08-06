using Prism.Ioc;

namespace FlowFusion.Module.Main.RibbonPage
{
    public partial class HumanResourcesRibbonPage
    {
        public HumanResourcesRibbonPage(IContainerExtension containerExtension)
        {
            InitializeComponent();
            DataContext = containerExtension.Resolve<HumanResourcesRibbonPageViewModel>();
        }
    }
}