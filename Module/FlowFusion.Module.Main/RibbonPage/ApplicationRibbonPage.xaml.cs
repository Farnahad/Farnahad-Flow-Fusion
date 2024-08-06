using Prism.Ioc;

namespace FlowFusion.Module.Main.RibbonPage
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