using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Folder.Folder;

namespace FlowFusion.Action.Folder;

public class RenameFolder(IFolderService folderService) : IAction
{
    public string Name => "Rename folder";

    public ActionInput FolderToRename { get; set; } = new();
    public ActionInput NewFolderName { get; set; } = new();
    public Variable RenamedFolder { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var folderToRenameValue = await sandBox.EvaluateActionInput<string>(FolderToRename);
        var newFolderNameValue = await sandBox.EvaluateActionInput<string>(NewFolderName);

        RenamedFolder.Value = folderService.RenameFolder(folderToRenameValue, newFolderNameValue);

        sandBox.SetVariable(RenamedFolder);
    }
}