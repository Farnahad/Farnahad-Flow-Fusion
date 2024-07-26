using ManaErp.Module.Core.General;
using ManaErp.Service.Core.Container;

namespace ManaErp.Module.Main.View.StatusBar
{
    public class StatusBarViewModel : GeneralViewModel
    {
        protected StatusBarViewModel(IContainerService containerService)
            : base(containerService)
        {
        }

        public override void Dispose()
        {
        }
    }
}