namespace ManaErp.Module.Main.RibbonPage
{
    public class BaseRibbonPageViewModel : GeneralViewModel
    {
        public BaseRibbonPageViewModel(IContainerService containerService)
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