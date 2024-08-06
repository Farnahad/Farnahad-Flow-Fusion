namespace ManaErp.Service.Main.MessageBox
{
    public interface IMessageBoxService : IService
    {
        void ShowInfo(string info, string title = null);
        bool? ShowYesNo(string question, string title = null);
        void ShowMethodResultMessage(MethodResult methodResult, string title = null);
        void ShowCaNotBeDeletedMessage(IDbModel dbModel, string title = null);
    }
}