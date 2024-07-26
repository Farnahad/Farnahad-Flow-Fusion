using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.System.GetWindowsEnvironmentVariableBase;

namespace FarnahadFlowFusion.Action.System;

public class GetWindowsEnvironmentVariable : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Get Windows environment variable";

    public ActionInput EnvironmentVariableName { get; set; }
    public bool SearchForVariableOnlyInScope { get; set; }
    public Scope Scope { get; set; }
    public Variable EnvironmentVariableValue { get; set; }

    public GetWindowsEnvironmentVariable()
    {
        _cSharpService = new CSharpService();

        EnvironmentVariableName = new ActionInput();
        SearchForVariableOnlyInScope = false;
        Scope = Scope.User;
        EnvironmentVariableValue = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var environmentVariableNameValue = await _cSharpService.EvaluateActionInput<string>(sandBox, EnvironmentVariableName);

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

        sandBox.Variables.Add(EnvironmentVariableValue);
    }
}