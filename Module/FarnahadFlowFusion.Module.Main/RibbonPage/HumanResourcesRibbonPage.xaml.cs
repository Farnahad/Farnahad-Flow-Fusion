using Prism.Ioc;

namespace ManaErp.Module.Main.RibbonPage
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