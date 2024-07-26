using Prism.Ioc;

namespace ManaErp.Module.Main.RibbonPage
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