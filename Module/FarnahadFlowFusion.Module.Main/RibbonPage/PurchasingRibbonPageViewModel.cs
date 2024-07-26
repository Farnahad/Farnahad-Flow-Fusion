using ManaErp.Module.Core.General;
using ManaErp.Service.Core.Container;

namespace ManaErp.Module.Main.RibbonPage
{
    public class PurchasingRibbonPageViewModel : GeneralViewModel
    {
        public PurchasingRibbonPageViewModel(IContainerService containerService)
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