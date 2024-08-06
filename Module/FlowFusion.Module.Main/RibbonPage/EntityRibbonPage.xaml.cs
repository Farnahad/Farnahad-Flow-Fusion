using Prism.Ioc;

namespace FlowFusion.Module.Main.RibbonPage
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