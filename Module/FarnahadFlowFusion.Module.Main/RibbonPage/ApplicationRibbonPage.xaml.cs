using Prism.Ioc;

namespace ManaErp.Module.Main.RibbonPage
{
    public partial class ApplicationRibbonPage
    {
        public ApplicationRibbonPage(IContainerExtension containerExtension)
        {
            InitializeComponent();
            DataContext = containerExtension.Resolve<ApplicationRibbonPageViewModel>();
        }
    }
}