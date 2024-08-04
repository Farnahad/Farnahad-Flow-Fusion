using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.Folder;

public class DeleteFolder : IAction
{
    public string Name => "Delete folder";

    public ActionInput FolderToDelete { get; set; }

    public DeleteFolder()
    {
        FolderToDelete = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var folderToDeleteValue = await sandBox.EvaluateActionInput<string>(FolderToDelete);

        var directoryInfo = new DirectoryInfo(folderToDeleteValue);

        if (directoryInfo.Exists)
            directoryInfo.Delete();
    }
}