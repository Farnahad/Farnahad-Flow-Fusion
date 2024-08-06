using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Folder;

public class DeleteFolder : IAction //XXXXXXXXXXXX
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