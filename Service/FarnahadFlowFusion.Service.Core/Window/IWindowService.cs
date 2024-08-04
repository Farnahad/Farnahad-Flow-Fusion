namespace ManaErp.Service.Main.Window
{
    public interface IWindowService : IService
    {
        void Show<T>() where T : View;
        void Show<T>(params Tuple<string, object>[] parameters) where T : View;
        void ShowDialog<T>() where T : View;
        void ShowDialog<T>(params Tuple<string, object>[] parameters) where T : View;
        void Close(ViewModel viewModel, WindowResult? windowResult = null);
    }
}