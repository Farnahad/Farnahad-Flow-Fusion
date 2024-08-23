using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.File.File;

namespace FlowFusion.Action.File;

public class GetFilePathPart(IFileService fileService) : IAction
{
    public string Name => "Get file path part";

    public ActionInput FilePath { get; set; } = new();
    public Variable RootPath { get; set; } = new();
    public Variable Directory { get; set; } = new();
    public Variable FileName { get; set; } = new();
    public Variable FileNameNoExtension { get; set; } = new();
    public Variable FileExtension { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

        fileService.GetFilePathPart(filePathValue, out var rootPath, out var directory,
            out var fileName, out var fileNameNoExtension, out var fileExtension);

        RootPath.Value = rootPath;
        Directory.Value = directory;
        FileName.Value = fileName;
        FileNameNoExtension.Value = fileNameNoExtension;
        FileExtension.Value = fileExtension;

        sandBox.SetVariable(RootPath);
        sandBox.SetVariable(Directory);
        sandBox.SetVariable(FileName);
        sandBox.SetVariable(FileNameNoExtension);
        sandBox.SetVariable(FileExtension);
    }
}