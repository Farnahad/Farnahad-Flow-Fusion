using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Folder;

public class MoveFolder : IAction
{
    public string Name => "Move folder";

    public ActionInput FolderToMove { get; set; }
    public ActionInput DestinationFolder { get; set; }
    public Variable MovedFolder { get; set; }

    public MoveFolder()
    {
        FolderToMove = new ActionInput();
        DestinationFolder = new ActionInput();
        MovedFolder = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var folderToMoveValue = await sandBox.EvaluateActionInput<string>(FolderToMove);
        var destinationFolderValue = await sandBox.EvaluateActionInput<string>(DestinationFolder);

        var directoryInfo = new DirectoryInfo(folderToMoveValue);
        directoryInfo.MoveTo(destinationFolderValue);

        MovedFolder.Value = destinationFolderValue;

        sandBox.SetVariable(MovedFolder);
    }
}