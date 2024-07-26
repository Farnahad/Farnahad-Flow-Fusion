using ManaErp.Module.Core.General;
using ManaErp.Service.Core.Container;

namespace ManaErp.Module.Main.RibbonPage
{
    public class EntityRibbonPageViewModel : GeneralViewModel
    {
        public EntityRibbonPageViewModel(IContainerService containerService)
            : base(containerService)
        {
        }

        protected override void InitialCommands()
        {
            base.InitialCommands();
        }

        public override void Dispose()
        {
        }
    }
}