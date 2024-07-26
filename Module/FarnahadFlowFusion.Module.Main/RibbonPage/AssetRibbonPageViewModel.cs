using ManaErp.Module.Core.General;
using ManaErp.Service.Core.Container;

namespace ManaErp.Module.Main.RibbonPage
{
    public class AssetRibbonPageViewModel : GeneralViewModel
    {
        public AssetRibbonPageViewModel(IContainerService containerService)
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