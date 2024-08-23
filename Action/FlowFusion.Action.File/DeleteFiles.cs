using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.File.File;

namespace FlowFusion.Action.File;

public class DeleteFiles(IFileService fileService) : IAction
{
    public string Name => "Delete file(s)";

    public ActionInput FilesToDelete { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var filesToDeleteValue = await sandBox.EvaluateActionInput<List<string>>(FilesToDelete);

        fileService.DeleteFiles(filesToDeleteValue);
    }
}