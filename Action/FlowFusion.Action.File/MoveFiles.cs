using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.File;

public class MoveFiles : IAction
{
    public string Name => "Move file(s)";

    public ActionInput FilesToMove { get; set; }
    public ActionInput DestinationFolder { get; set; }
    public MoveFilesBase.IfFileExists IfFileExists { get; set; }
    public Variable MovedFiles { get; set; }

    public MoveFiles()
    {
        FilesToMove = new ActionInput();
        DestinationFolder = new ActionInput();
        IfFileExists = MoveFilesBase.IfFileExists.DoNothing;
        MovedFiles = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filesToMoveValue = await sandBox.EvaluateActionInput<List<string>>(FilesToMove);
        var destinationFolderValue = await sandBox.EvaluateActionInput<string>(DestinationFolder);

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

        sandBox.SetVariable(MovedFiles);
    }
}