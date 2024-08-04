using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.File;

public class GetFilePathPart : IAction
{
    public string Name => "Get file path part";

    public ActionInput FilePath { get; set; }
    public Variable RootPath { get; set; }
    public Variable Directory { get; set; }
    public Variable FileName { get; set; }
    public Variable FileNameNoExtension { get; set; }
    public Variable FileExtension { get; set; }

    public GetFilePathPart()
    {
        FilePath = new ActionInput();
        RootPath = new Variable();
        Directory = new Variable();
        FileName = new Variable();
        FileNameNoExtension = new Variable();
        FileExtension = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

        var fileInfo = new FileInfo(filePathValue);

        RootPath.Value = fileInfo.FullName;
        Directory.Value = fileInfo.Directory?.FullName;
        FileName.Value = fileInfo.Name;
        FileNameNoExtension.Value = fileInfo.Name.Replace(fileInfo.Extension, "");
        FileExtension.Value = fileInfo.Extension;

        sandBox.SetVariable(RootPath);
        sandBox.SetVariable(Directory);
        sandBox.SetVariable(FileName);
        sandBox.SetVariable(FileNameNoExtension);
        sandBox.SetVariable(FileExtension);
    }
}