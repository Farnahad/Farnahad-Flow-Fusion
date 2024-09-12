using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.System.System;
using FlowFusion.Service.System.System.Base;

namespace FlowFusion.Action.System;

public class SetWindowsEnvironmentVariable(ISystemService systemService) : IAction
{
    public string Name => "Set Windows environment variable";

    public ActionInput EnvironmentVariableName { get; set; } = new();
    public ActionInput NewEnvironmentVariableValue { get; set; } = new();
    public WindowsEnvironmentVariableType Type { get; set; } = WindowsEnvironmentVariableType.User;

    public async Task Execute(SandBox sandBox)
    {
        var environmentVariableNameValue = await sandBox.EvaluateActionInput<string>(EnvironmentVariableName);
        var newEnvironmentVariableValueValue = await sandBox.EvaluateActionInput<string>(NewEnvironmentVariableValue);

        systemService.SetWindowsEnvironmentVariable(environmentVariableNameValue, newEnvironmentVariableValueValue, Type);
    }
}