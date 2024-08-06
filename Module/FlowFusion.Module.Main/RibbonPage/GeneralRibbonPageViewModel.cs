namespace FlowFusion.Module.Main.RibbonPage
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