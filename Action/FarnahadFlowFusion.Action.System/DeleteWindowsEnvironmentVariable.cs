using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using Type = FarnahadFlowFusion.Action.System.DeleteWindowsEnvironmentVariableBase.Type;

namespace FarnahadFlowFusion.Action.System;

public class DeleteWindowsEnvironmentVariable : IAction
{
    public string Name => "Delete Windows environment variable";

    public ActionInput EnvironmentVariableName { get; set; }
    public DeleteWindowsEnvironmentVariableBase.Type Type { get; set; }

    public DeleteWindowsEnvironmentVariable()
    {
        EnvironmentVariableName = new ActionInput();
        Type = DeleteWindowsEnvironmentVariableBase.Type.User;
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