using Prism.Ioc;

namespace ManaErp.Module.Main.RibbonPage
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