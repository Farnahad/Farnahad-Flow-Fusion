using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.System.System;
using FlowFusion.Service.System.System.Base;

namespace FlowFusion.Action.System;

public class GetWindowsEnvironmentVariable(ISystemService systemService) : IAction
{
    public string Name => "Get Windows environment variable";

    public ActionInput EnvironmentVariableName { get; set; } = new();
    public bool SearchForVariableOnlyInScope { get; set; } = false;
    public Scope Scope { get; set; } = Scope.User;
    public Variable EnvironmentVariableValue { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var environmentVariableNameValue = await sandBox.EvaluateActionInput<string>(EnvironmentVariableName);

        if (SearchForVariableOnlyInScope)
        {
            EnvironmentVariableValue.Value = Environment.GetEnvironmentVariable(
                environmentVariableNameValue, EnvironmentVariableTarget.Machine);

            if (EnvironmentVariableValue.Value == null)
            {
                EnvironmentVariableValue.Value = Environment.GetEnvironmentVariable(
                    environmentVariableNameValue, EnvironmentVariableTarget.User);
            }
        }
        else
        {
            switch (Scope)
            {
                case Scope.System:
                    EnvironmentVariableValue.Value = Environment.GetEnvironmentVariable(
                        environmentVariableNameValue, EnvironmentVariableTarget.Machine);
                    break;
                case Scope.User:
                    EnvironmentVariableValue.Value = Environment.GetEnvironmentVariable(
                        environmentVariableNameValue, EnvironmentVariableTarget.User);
                    break;
            }
        }

        sandBox.SetVariable(EnvironmentVariableValue);
    }
}