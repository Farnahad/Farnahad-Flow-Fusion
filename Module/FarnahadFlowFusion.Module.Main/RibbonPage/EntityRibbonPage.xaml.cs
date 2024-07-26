using Prism.Ioc;

namespace ManaErp.Module.Main.RibbonPage
{
    public partial class EntityRibbonPage
    {
        public EntityRibbonPage(IContainerExtension containerExtension)
        {
            InitializeComponent();
            DataContext = containerExtension.Resolve<EntityRibbonPageViewModel>();
        }
    }
}