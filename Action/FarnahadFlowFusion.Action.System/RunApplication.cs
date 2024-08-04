using System.Diagnostics;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Action.System.RunApplicationBase;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.System;

public class RunApplication : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Run application";

    public ActionInput ApplicationPath { get; set; }
    public ActionInput CommandLineArguments { get; set; }
    public ActionInput WorkingFolder { get; set; }
    public RunApplicationBase.WindowStyle WindowStyle { get; set; }
    public AfterApplicationLunch AfterApplicationLunch { get; set; }
    public ActionInput Timeout { get; set; }
    public Variable AppProcessId { get; set; }

    public RunApplication()
    {
        _cSharpService = new CSharpService();

        ApplicationPath = new ActionInput();
        CommandLineArguments = new ActionInput();
        WorkingFolder = new ActionInput();
        WindowStyle = WindowStyle.Normal;
        AfterApplicationLunch = AfterApplicationLunch.ContinueImmediately;
        Timeout = new ActionInput();
        AppProcessId = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var applicationPathValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ApplicationPath);
        var commandLineArgumentsValue = await _cSharpService.EvaluateActionInput<string>(sandBox, CommandLineArguments);
        var workingFolderValue = await _cSharpService.EvaluateActionInput<string>(sandBox, WorkingFolder);
        var timeoutValue = await _cSharpService.EvaluateActionInput<int>(sandBox, Timeout);


        var processWindowStyle = ProcessWindowStyle.Normal;

        switch (WindowStyle)
        {
            case WindowStyle.Hidden:
                processWindowStyle = ProcessWindowStyle.Hidden;
                break;
            case WindowStyle.Maximized:
                processWindowStyle = ProcessWindowStyle.Maximized;
                break;
            case WindowStyle.Minimized:
                processWindowStyle = ProcessWindowStyle.Minimized;
                break;
            case WindowStyle.Normal:
                processWindowStyle = ProcessWindowStyle.Normal;
                break;
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = applicationPathValue,
            Arguments = commandLineArgumentsValue,
            WorkingDirectory = workingFolderValue,
            WindowStyle = processWindowStyle
        };

        using (var process = new Process())
        {
            process.StartInfo = startInfo;
            process.Start();
            AppProcessId.Value = process.Id;

            switch (AfterApplicationLunch)
            {
                case AfterApplicationLunch.ContinueImmediately:
                    break;

                case AfterApplicationLunch.WaitForApplicationToComplete:
                    if (timeoutValue != 0)
                    {
                        if (await Task.Run(() => process.WaitForExit(TimeSpan.FromSeconds(timeoutValue))))
                        {
                        }
                        else
                        {
                            process.Kill();
                            throw new TimeoutException("The application did not exit in the specified time.");
                        }
                    }
                    else
                    {
                        await process.WaitForExitAsync();
                    }
                    break;

                case AfterApplicationLunch.WaitForApplicationToLoad:
                    // Implement a mechanism to check if the application has fully loaded
                    // This is a placeholder and may need a more specific approach depending on the application
                    await Task.Delay(5000); // Simulate wait for the application to load
                    break;
            }
        }

        sandBox.Variables.Add(AppProcessId);
    }
}