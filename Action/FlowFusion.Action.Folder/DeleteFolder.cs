using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Folder.Folder;

namespace FlowFusion.Action.Folder;

public class DeleteFolder(IFolderService folderService) : IAction
{
    public string Name => "Delete folder";

    public ActionInput FolderToDelete { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var folderToDeleteValue = await sandBox.EvaluateActionInput<string>(FolderToDelete);
        folderService.DeleteFolder(folderToDeleteValue);
    }
}