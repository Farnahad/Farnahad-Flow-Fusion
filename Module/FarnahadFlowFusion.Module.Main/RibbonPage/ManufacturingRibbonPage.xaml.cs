using Prism.Ioc;

namespace ManaErp.Module.Main.RibbonPage
{
    public partial class ManufacturingRibbonPage
    {
        public ManufacturingRibbonPage(IContainerExtension containerExtension)
        {
            InitializeComponent();
            DataContext = containerExtension.Resolve<ManufacturingRibbonPageViewModel>();
        }
    }
}