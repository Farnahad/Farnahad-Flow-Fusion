namespace FlowFusion.Module.Main.RibbonPage
{
    public class CrmRibbonPageViewModel : GeneralViewModel
    {
        public CrmRibbonPageViewModel(IContainerService containerService)
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