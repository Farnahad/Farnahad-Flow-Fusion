using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Folder.Folder;

namespace FlowFusion.Action.Folder;

public class MoveFolder(IFolderService folderService) : IAction
{
    public string Name => "Move folder";

    public ActionInput FolderToMove { get; set; } = new();
    public ActionInput DestinationFolder { get; set; } = new();
    public Variable MovedFolder { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var folderToMoveValue = await sandBox.EvaluateActionInput<string>(FolderToMove);
        var destinationFolderValue = await sandBox.EvaluateActionInput<string>(DestinationFolder);

        MovedFolder.Value = folderService.MoveFolder(folderToMoveValue, destinationFolderValue);

        sandBox.SetVariable(MovedFolder);
    }
}