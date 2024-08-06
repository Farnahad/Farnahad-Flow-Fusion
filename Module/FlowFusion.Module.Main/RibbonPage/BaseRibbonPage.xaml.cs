using Prism.Ioc;

namespace FlowFusion.Module.Main.RibbonPage
{
    public partial class BaseRibbonPage
    {
        public BaseRibbonPage(IContainerExtension containerExtension)
        {
            InitializeComponent();
            DataContext = containerExtension.Resolve<BaseRibbonPageViewModel>();
        }
    }
}