﻿namespace ManaErp.Module.Main.RibbonPage
{
    public class ApplicationRibbonPageViewModel : GeneralViewModel
    {
        public ApplicationRibbonPageViewModel(IContainerService containerService)
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