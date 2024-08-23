using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.System.System.Base;

namespace FlowFusion.Action.System;

public class SetWindowsEnvironmentVariable : IAction //XXXXXXXXXXXX
{
    public string Name => "Set Windows environment variable";

    public ActionInput EnvironmentVariableName { get; set; }
    public ActionInput NewEnvironmentVariableValue { get; set; }
    public WindowsEnvironmentVariableType Type { get; set; }

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