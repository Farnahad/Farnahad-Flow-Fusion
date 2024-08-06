using System.Text.RegularExpressions;

namespace FlowFusion.Service.Main.Utility;

public class UtilityService
{
    public string SeparateCamelCase(string input)
    {
        return Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");
    }
}