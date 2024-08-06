using Prism.Ioc;

namespace FlowFusion.Module.Main.RibbonPage
{
    public partial class FinanceRibbonPage
    {
        public FinanceRibbonPage(IContainerExtension containerExtension)
        {
            InitializeComponent();
            DataContext = containerExtension.Resolve<FinanceRibbonPageViewModel>();
        }
    }
}