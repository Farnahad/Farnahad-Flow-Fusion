namespace ManaErp.Service.Main.Text
{
    public class TextService : ITextService
    {
        public string GetEditTitle(string modelName, string modelProperty)
        {
            return string.IsNullOrWhiteSpace(modelProperty) ?
                "Edit " + modelName : "Edit " + modelName + " - " + modelProperty;
        }

        public string GetAddTitle(string modelName)
        {
            return modelName + " New";
        }

        public string GetListTitle(string modelName)
        {
            return "List " + modelName;
        }

        public void Dispose()
        {
        }
    }
}