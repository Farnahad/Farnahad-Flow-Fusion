using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Action.System.GetWindowsEnvironmentVariableBase;

namespace FlowFusion.Action.System;

public class GetWindowsEnvironmentVariable : IAction //XXXXXXXXXXXX
{
    public string Name => "Get Windows environment variable";

    public ActionInput EnvironmentVariableName { get; set; }
    public bool SearchForVariableOnlyInScope { get; set; }
    public Scope Scope { get; set; }
    public Variable EnvironmentVariableValue { get; set; }

    public GetWindowsEnvironmentVariable()
    {
        EnvironmentVariableName = new ActionInput();
        SearchForVariableOnlyInScope = false;
        Scope = Scope.User;
        EnvironmentVariableValue = new Variable();
    }

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