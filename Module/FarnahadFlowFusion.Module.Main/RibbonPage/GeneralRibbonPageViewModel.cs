using ManaErp.Core.Control.DockLayout;
using ManaErp.Core.Main.Command;
using ManaErp.Module.Core.General;
using ManaErp.Module.General.Location;
using ManaErp.Service.Core.Container;

namespace ManaErp.Module.Main.RibbonPage
{
    public class GeneralRibbonPageViewModel : GeneralViewModel
    {
        public GeneralRibbonPageViewModel(IContainerService containerService)
            : base(containerService)
        {
        }

        public MeCommand ShowLocationListViewCommand { get; set; }

        protected override void InitialCommands()
        {
            base.InitialCommands();

            ShowLocationListViewCommand = new MeCommand(ShowLocationListView);
        }

        private void ShowLocationListView()
        {
            NavigationService.NavigateDock<LocationListView>(MeDockPotion.Document);
        }

        public override void Dispose()
        {
        }
    }
}