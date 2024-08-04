using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Folder;

public class RenameFolder : IAction
{
    public string Name => "Rename folder";

    public ActionInput FolderToRename { get; set; }
    public ActionInput NewFolderName { get; set; }
    public Variable RenamedFolder { get; set; }

    public RenameFolder()
    {
        FolderToRename = new ActionInput();
        NewFolderName = new ActionInput();
        RenamedFolder = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var folderToRenameValue = await sandBox.EvaluateActionInput<string>(FolderToRename);
        var newFolderNameValue = await sandBox.EvaluateActionInput<string>(NewFolderName);

        var directoryInfo = new DirectoryInfo(folderToRenameValue);

        if (directoryInfo.Exists == false)
            directoryInfo.Create();

        directoryInfo.MoveTo(newFolderNameValue);
    }
}