using FarnahadFlowFusion.Action.Main;
using Type = FarnahadFlowFusion.Action.System.DeleteWindowsEnvironmentVariableBase.Type;

namespace FarnahadFlowFusion.Action.System;

public class DeleteWindowsEnvironmentVariable : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Delete Windows environment variable";

    public ActionInput EnvironmentVariableName { get; set; }
    public DeleteWindowsEnvironmentVariableBase.Type Type { get; set; }

    public DeleteWindowsEnvironmentVariable()
    {
        _cSharpService = new CSharpService();

        EnvironmentVariableName = new ActionInput();
        Type = DeleteWindowsEnvironmentVariableBase.Type.User;
    }

    public async Task Execute(SandBox sandBox)
    {
        var environmentVariableNameValue = await _cSharpService.EvaluateActionInput<string>(sandBox, EnvironmentVariableName);

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