using Prism.Ioc;

namespace ManaErp.Module.Main.RibbonPage
{
    public partial class AssetRibbonPage
    {
        public AssetRibbonPage(IContainerExtension containerExtension)
        {
            InitializeComponent();
            DataContext = containerExtension.Resolve<AssetRibbonPageViewModel>();
        }
    }
}