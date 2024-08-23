using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Folder.Folder;

namespace FlowFusion.Action.Folder;

public class GetSubfoldersInFolder(IFolderService folderService) : IAction
{
    public string Name => "Get subfolders in folder";

    public ActionInput Folder { get; set; } = new();
    public ActionInput FolderFilter { get; set; } = new();
    public bool IncludeSubfolders { get; set; } = false;
    public Variable Folders { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var folderValue = await sandBox.EvaluateActionInput<string>(Folder);
        var folderFilterValue = await sandBox.EvaluateActionInput<string>(FolderFilter);

        Folders.Value = folderService.GetSubfoldersInFolder(folderValue, folderFilterValue, IncludeSubfolders);

        sandBox.SetVariable(Folders);
    }
}