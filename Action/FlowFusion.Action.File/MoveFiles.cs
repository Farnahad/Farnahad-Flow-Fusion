using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.File.File;

namespace FlowFusion.Action.File;

public class MoveFiles(IFileService fileService) : IAction
{
    public string Name => "Move file(s)";

    public ActionInput FilesToMove { get; set; } = new();
    public ActionInput DestinationFolder { get; set; } = new();
    public Service.File.File.Base.IfFileExists IfFileExists { get; set; } = Service.File.File.Base.IfFileExists.DoNothing;
    public Variable MovedFiles { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var filesToMoveValue = await sandBox.EvaluateActionInput<List<string>>(FilesToMove);
        var destinationFolderValue = await sandBox.EvaluateActionInput<string>(DestinationFolder);

        MovedFiles.Value = fileService.MoveFiles(filesToMoveValue, destinationFolderValue, IfFileExists);

        sandBox.SetVariable(MovedFiles);
    }
}