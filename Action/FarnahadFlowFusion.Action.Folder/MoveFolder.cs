using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Folder;

public class MoveFolder : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Move folder";

    public ActionInput FolderToMove { get; set; }
    public ActionInput DestinationFolder { get; set; }
    public Variable MovedFolder { get; set; }

    public MoveFolder()
    {
        _cSharpService = new CSharpService();

        FolderToMove = new ActionInput();
        DestinationFolder = new ActionInput();
        MovedFolder = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var folderToMoveValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FolderToMove);
        var destinationFolderValue = await _cSharpService.EvaluateActionInput<string>(sandBox, DestinationFolder);

        var directoryInfo = new DirectoryInfo(folderToMoveValue);
        directoryInfo.MoveTo(destinationFolderValue);

        MovedFolder.Value = destinationFolderValue;

        sandBox.Variables.Add(MovedFolder);
    }
}