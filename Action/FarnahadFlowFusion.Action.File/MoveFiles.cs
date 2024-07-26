using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.File;

public class MoveFiles : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Move file(s)";

    public ActionInput FilesToMove { get; set; }
    public ActionInput DestinationFolder { get; set; }
    public MoveFilesBase.IfFileExists IfFileExists { get; set; }
    public Variable MovedFiles { get; set; }

    public MoveFiles()
    {
        _cSharpService = new CSharpService();

        FilesToMove = new ActionInput();
        DestinationFolder = new ActionInput();
        IfFileExists = MoveFilesBase.IfFileExists.DoNothing;
        MovedFiles = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filesToMoveValue = await _cSharpService.EvaluateActionInput<List<string>>(sandBox, FilesToMove);
        var destinationFolderValue = await _cSharpService.EvaluateActionInput<string>(sandBox, DestinationFolder);

        var movedFiles = new List<string>();

        foreach (var fileToMove in filesToMoveValue)
        {
            var fileInfo = new FileInfo(fileToMove);

            if ((fileInfo.Exists && IfFileExists == MoveFilesBase.IfFileExists.Overwrite) || fileInfo.Exists == false)
            {
                fileInfo.MoveTo(destinationFolderValue);
                movedFiles.Add(Path.Combine(destinationFolderValue, fileInfo.Name));
            }
        }

        MovedFiles.Value = movedFiles;

        sandBox.Variables.Add(MovedFiles);
    }
}