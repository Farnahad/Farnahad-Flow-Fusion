using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Folder.Folder;

namespace FlowFusion.Action.Folder;

public class GetFilesInFolder(IFolderService folderService) : IAction
{
    public string Name => "Get files in folder";

    public ActionInput Folder { get; set; } = new();
    public ActionInput FileFilter { get; set; } = new();
    public bool IncludeSubfolders { get; set; } = false;
    public Variable Files { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var folderValue = await sandBox.EvaluateActionInput<string>(Folder);
        var fileFilterValue = await sandBox.EvaluateActionInput<string>(FileFilter);

        Files.Value = folderService.GetFilesInFolder(folderValue, fileFilterValue, IncludeSubfolders);

        sandBox.SetVariable(Files);
    }
}