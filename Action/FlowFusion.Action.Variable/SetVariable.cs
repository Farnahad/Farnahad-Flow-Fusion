﻿using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Variable;

public class SetVariable : IAction
{
    private readonly VariableService _variableService;

    public string Name => "Set Variable";

    public string Variable { get; set; }
    public string Value { get; set; }

    public SetVariable()
    {
        _variableService = new VariableService();
    }

    public async Task Execute(SandBox sandBox)
    {
        var variableType = _variableService.GetVariableType(Value);

        object value = variableType switch
        {
            VariableType.Boolean => bool.Parse(Value),
            VariableType.Double => double.Parse(Value),
            VariableType.Integer => int.Parse(Value),
            VariableType.String => Value,
            _ => null
        };

        sandBox.SetVariable(new Main.Variable.Variable(Variable, value));
        await Task.CompletedTask;
    }
}