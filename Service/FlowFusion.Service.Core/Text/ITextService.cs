namespace FlowFusion.Service.Core.Text
{
    public interface ITextService : IService
    {
        string GetEditTitle(string modelName, string modelProperty);
        string GetAddTitle(string modelName);
        string GetListTitle(string modelName);
    }
}