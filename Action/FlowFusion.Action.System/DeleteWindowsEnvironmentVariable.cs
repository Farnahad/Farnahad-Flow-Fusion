using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.System.System.Base;

namespace FlowFusion.Action.System;

public class DeleteWindowsEnvironmentVariable : IAction
{
    public string Name => "Delete Windows environment variable";

    public ActionInput EnvironmentVariableName { get; set; }
    public WindowsEnvironmentVariableType Type { get; set; }

    public DeleteWindowsEnvironmentVariable()
    {
        EnvironmentVariableName = new ActionInput();
        Type = WindowsEnvironmentVariableType.User;
    }

    public async Task Execute(SandBox sandBox)
    {
        var environmentVariableNameValue = await sandBox.EvaluateActionInput<string>(EnvironmentVariableName);

        switch (Type)
        {
            case WindowsEnvironmentVariableType.System:
                Environment.SetEnvironmentVariable(environmentVariableNameValue, null, EnvironmentVariableTarget.Machine);
                break;
            case WindowsEnvironmentVariableType.User:
                Environment.SetEnvironmentVariable(environmentVariableNameValue, null, EnvironmentVariableTarget.User);
                break;
        }
    }
}