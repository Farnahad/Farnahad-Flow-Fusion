using Prism.Ioc;

namespace ManaErp.Module.Main.RibbonPage
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