namespace FarnahadFlowFusion.Service.Main.Variable;

class VariableService
{
    public VariableType GetVariableType(string value)
    {
        if (bool.TryParse(value, out _) &&
            (value.Equals("true", StringComparison.InvariantCultureIgnoreCase) ||
             value.Equals("false", StringComparison.InvariantCultureIgnoreCase)))
        {
            return VariableType.Boolean;
        }

        if (int.TryParse(value, out _))
            return VariableType.Integer;

        if (double.TryParse(value, out _))
            return VariableType.Double;

        return VariableType.String;
    }
}