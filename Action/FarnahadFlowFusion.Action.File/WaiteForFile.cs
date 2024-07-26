using FarnahadFlowFusion.Action.File.WaiteForFileBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.File;

public class WaiteForFile : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Waite for file";

    public WaiteForFileToBe WaiteForFileToBe { get; set; }
    public ActionInput FilePath { get; set; }
    public bool FailWithTimeoutError { get; set; }
    public ActionInput Duration { get; set; }

    public WaiteForFile()
    {
        _cSharpService = new CSharpService();

        WaiteForFileToBe = WaiteForFileToBe.Created;
        FilePath = new ActionInput();
        FailWithTimeoutError = false;
        Duration = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FilePath);
        var durationValue = await _cSharpService.EvaluateActionInput<int>(sandBox, Duration);

        var timeout = TimeSpan.FromMilliseconds(durationValue);

        using var cancellationTokenSource = new CancellationTokenSource(timeout);
        var fileWatchTask = WaiteForFileToBe == WaiteForFileToBe.Created ?
            WaitForFileCreationAsync(filePathValue, cancellationTokenSource.Token) :
            WaitForFileDeletionAsync(filePathValue, cancellationTokenSource.Token);

        await fileWatchTask;
    }

    private Task WaitForFileCreationAsync(string filePath, CancellationToken token)
    {
        return Task.Run(async () =>
        {
            while (!global::System.IO.File.Exists(filePath))
            {
                token.ThrowIfCancellationRequested();
                await Task.Delay(100, token);
            }
        }, token);
    }

    private Task WaitForFileDeletionAsync(string filePath, CancellationToken token)
    {
        return Task.Run(async () =>
        {
            while (global::System.IO.File.Exists(filePath))
            {
                token.ThrowIfCancellationRequested();
                await Task.Delay(100, token);
            }
        }, token);
    }
}