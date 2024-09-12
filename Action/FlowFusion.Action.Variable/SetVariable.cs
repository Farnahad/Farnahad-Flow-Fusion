using System.Data;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Variable.Variable;

namespace FlowFusion.Action.Variable;

public class SetVariable(IVariableService variableService) : GeneralAction
{
    public override string Name => "Set Variable";

    public string Variable { get; set; }
    public string Value { get; set; }

    public override async Task Execute(SandBox sandBox)
    {
        var variableType = variableService.GetVariableType(Value);

        var newVariable = new Main.Variable.Variable(Variable, null);

        object value = variableType switch
        {
            VariableType.Text => newVariable.Value = Value,
            VariableType.Number => newVariable.Value = double.Parse(Value),
            VariableType.Boolean => newVariable.Value = bool.Parse(Value),
            VariableType.CustomObject => newVariable.Value = (object)Value,
            VariableType.List => newVariable.Value = new List<object>(),
            VariableType.DataTable => newVariable.Value = new DataTable(),
            _ => newVariable.Value = null
        };

        if (newVariable.Value != null)
            sandBox.SetVariable(newVariable);

        await Task.CompletedTask;
    }
}