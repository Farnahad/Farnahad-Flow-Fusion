using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.File;

public class CopyFiles : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Copy file(s)";

    public ActionInput FilesToCopy { get; set; }
    public ActionInput DestinationFolder { get; set; }
    public CopyFilesBase.IfFileExists IfFileExists { get; set; }
    public Variable CopiedFiles { get; set; }

    public CopyFiles()
    {
        _cSharpService = new CSharpService();

        FilesToCopy = new ActionInput();
        DestinationFolder = new ActionInput();
        IfFileExists = CopyFilesBase.IfFileExists.DoNothing;
        CopiedFiles = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filesToCopyValue = await _cSharpService.EvaluateActionInput<List<string>>(sandBox, FilesToCopy);
        var destinationFolder = await _cSharpService.EvaluateActionInput<string>(sandBox, DestinationFolder);

        var copiedFiles = new List<string>();

        foreach (var fileToCopy in filesToCopyValue)
        {
            var fileInfo = new FileInfo(fileToCopy);

            if ((fileInfo.Exists && IfFileExists == CopyFilesBase.IfFileExists.Overwrite) ||
                fileInfo.Exists == false)
            {
                fileInfo.CopyTo(destinationFolder);
                copiedFiles.Add(Path.Combine(destinationFolder, fileInfo.Name));
            }
        }

        CopiedFiles.Value = copiedFiles;

        sandBox.Variables.Add(CopiedFiles);
    }
}