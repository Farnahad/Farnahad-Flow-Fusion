using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.System.System;
using FlowFusion.Service.System.System.Base;

namespace FlowFusion.Action.System;

public class RunApplication(ISystemService systemService) : IAction
{
    public string Name => "Run application";

    public ActionInput ApplicationPath { get; set; } = new();
    public ActionInput CommandLineArguments { get; set; } = new();
    public ActionInput WorkingFolder { get; set; } = new();
    public WindowStyle WindowStyle { get; set; } = WindowStyle.Normal;
    public AfterApplicationLunch AfterApplicationLunch { get; set; } = AfterApplicationLunch.ContinueImmediately;
    public ActionInput Timeout { get; set; } = new();
    public Variable AppProcessId { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var applicationPathValue = await sandBox.EvaluateActionInput<string>(ApplicationPath);
        var commandLineArgumentsValue = await sandBox.EvaluateActionInput<string>(CommandLineArguments);
        var workingFolderValue = await sandBox.EvaluateActionInput<string>(WorkingFolder);
        var timeoutValue = await sandBox.EvaluateActionInput<int>(Timeout);

        AppProcessId.Value = systemService.RunApplication(applicationPathValue, commandLineArgumentsValue,
            workingFolderValue, WindowStyle, AfterApplicationLunch, timeoutValue);

        sandBox.SetVariable(AppProcessId);
    }
}