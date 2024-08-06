using Prism.Ioc;

namespace ManaErp.Module.Main.RibbonPage
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