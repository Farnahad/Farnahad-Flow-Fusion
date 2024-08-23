using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.File.File;

namespace FlowFusion.Action.File;

public class CopyFiles(IFileService fileService) : IAction
{
    public string Name => "Copy file(s)";

    public ActionInput FilesToCopy { get; set; } = new();
    public ActionInput DestinationFolder { get; set; } = new();
    public Service.File.File.Base.IfFileExists IfFileExists { get; set; } = Service.File.File.Base.IfFileExists.DoNothing;
    public Variable CopiedFiles { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var filesToCopyValue = await sandBox.EvaluateActionInput<List<string>>(FilesToCopy);
        var destinationFolder = await sandBox.EvaluateActionInput<string>(DestinationFolder);

        CopiedFiles.Value = fileService.CopyFiles(filesToCopyValue, destinationFolder, IfFileExists);

        sandBox.SetVariable(CopiedFiles);
    }
}