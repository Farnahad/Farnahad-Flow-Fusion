using Prism.Ioc;

namespace FlowFusion.Module.Main.RibbonPage
{
    public partial class GeneralRibbonPage
    {
        public GeneralRibbonPage(IContainerExtension containerExtension)
        {
            InitializeComponent();
            DataContext = containerExtension.Resolve<GeneralRibbonPageViewModel>();
        }
    }
}