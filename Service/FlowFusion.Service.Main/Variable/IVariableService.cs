using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Service.Main.Variable;

public interface IVariableService
{
    VariableType GetVariableType(string value);
}