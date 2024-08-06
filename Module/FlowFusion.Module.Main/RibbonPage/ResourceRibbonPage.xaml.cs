using Prism.Ioc;

namespace FlowFusion.Module.Main.RibbonPage
{
    public partial class ResourceRibbonPage
    {
        public ResourceRibbonPage(IContainerExtension containerExtension)
        {
            InitializeComponent();
            DataContext = containerExtension.Resolve<ResourceRibbonPageViewModel>();
        }
    }
}