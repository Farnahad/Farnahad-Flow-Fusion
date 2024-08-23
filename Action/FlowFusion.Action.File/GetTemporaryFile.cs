using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.File.File;

namespace FlowFusion.Action.File;

public class GetTemporaryFile(IFileService fileService) : IAction
{
    public string Name => "Get temporary file";

    public Variable TempFile { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        TempFile.Value = fileService.GetTemporaryFile();

        sandBox.SetVariable(TempFile);
        await Task.CompletedTask;
    }
}