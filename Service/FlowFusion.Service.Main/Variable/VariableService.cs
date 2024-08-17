using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Service.Main.Variable;

public class VariableService : IVariableService
{
    public VariableType GetVariableType(string value)
    {
        if (bool.TryParse(value, out _) &&
            (value.Equals("true", StringComparison.InvariantCultureIgnoreCase) ||
             value.Equals("false", StringComparison.InvariantCultureIgnoreCase)))
        {
            return VariableType.Boolean;
        }

        if (double.TryParse(value, out _))
            return VariableType.Number;

        return VariableType.Text;
        // return VariableType.CustomObject;
        // return VariableType.DataTable;
        // return VariableType.List;
    }
}