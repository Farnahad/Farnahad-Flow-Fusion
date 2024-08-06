using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using Type = FarnahadFlowFusion.Action.System.SetWindowsEnvironmentVariableBase.Type;

namespace FarnahadFlowFusion.Action.System;

public class SetWindowsEnvironmentVariable : IAction
{
    public string Name => "Set Windows environment variable";

    public ActionInput EnvironmentVariableName { get; set; }
    public ActionInput NewEnvironmentVariableValue { get; set; }
    public SetWindowsEnvironmentVariableBase.Type Type { get; set; }

    public SetWindowsEnvironmentVariable()
    {
        EnvironmentVariableName = new ActionInput();
        NewEnvironmentVariableValue = new ActionInput();
        Type = Type.User;
    }

    public async Task Execute(SandBox sandBox)
    {
        var environmentVariableNameValue = await sandBox.EvaluateActionInput<string>(EnvironmentVariableName);

        switch (Type)
        {
            case Type.System:
                Environment.SetEnvironmentVariable(environmentVariableNameValue, null, EnvironmentVariableTarget.Machine);
                break;
            case Type.User:
                Environment.SetEnvironmentVariable(environmentVariableNameValue, null, EnvironmentVariableTarget.User);
                break;
        }
    }
}