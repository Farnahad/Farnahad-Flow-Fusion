using FarnahadFlowFusion.Action.Main;
using Type = FarnahadFlowFusion.Action.System.SetWindowsEnvironmentVariableBase.Type;

namespace FarnahadFlowFusion.Action.System;

public class SetWindowsEnvironmentVariable : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Set Windows environment variable";

    public ActionInput EnvironmentVariableName { get; set; }
    public ActionInput NewEnvironmentVariableValue { get; set; }
    public SetWindowsEnvironmentVariableBase.Type Type { get; set; }

    public SetWindowsEnvironmentVariable()
    {
        _cSharpService = new CSharpService();

        EnvironmentVariableName = new ActionInput();
        NewEnvironmentVariableValue = new ActionInput();
        Type = Type.User;
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