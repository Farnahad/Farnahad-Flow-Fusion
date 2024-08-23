using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.File.File;
using FlowFusion.Service.File.File.Base;

namespace FlowFusion.Action.File;

public class WaiteForFile(IFileService fileService) : IAction
{
    public string Name => "Waite for file";

    public WaiteForFileToBe WaiteForFileToBe { get; set; } = WaiteForFileToBe.Created;
    public ActionInput FilePath { get; set; } = new();
    public bool FailWithTimeoutError { get; set; } = false;
    public ActionInput Duration { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);
        var durationValue = await sandBox.EvaluateActionInput<int>(Duration);

        await fileService.WaiteForFile(WaiteForFileToBe, filePathValue, FailWithTimeoutError, durationValue);
    }
}