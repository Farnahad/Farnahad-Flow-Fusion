namespace FlowFusion.Service.Core.Utility
{
    public interface IUtilityService : IService
    {
        string GetMd5Hash(string value);
    }
}