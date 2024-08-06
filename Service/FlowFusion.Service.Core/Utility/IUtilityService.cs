namespace ManaErp.Service.Main.Utility
{
    public interface IUtilityService : IService
    {
        string GetMd5Hash(string value);
    }
}