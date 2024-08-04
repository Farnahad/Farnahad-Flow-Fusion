namespace ManaErp.Service.Main.Navigation
{
    public interface INavigationService : IService
    {
        void NavigateRibbon<T>() where T : MeRibbonPage;
        void NavigateDock<T>(MeDockPotion meDockPotion, MeDockType meDockType = MeDockType.Visible) where T : View;
        void NavigateDock<T>(MeDockPotion meDockPotion, params Tuple<string, object>[] parameters) where T : View;
        void NavigateStatusBar<T>() where T : View;
    }
}