using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.File;

public class GetFilePathPart : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Get file path part";

    public ActionInput FilePath { get; set; }
    public Variable RootPath { get; set; }
    public Variable Directory { get; set; }
    public Variable FileName { get; set; }
    public Variable FileNameNoExtension { get; set; }
    public Variable FileExtension { get; set; }

    public GetFilePathPart()
    {
        _cSharpService = new CSharpService();

        FilePath = new ActionInput();
        RootPath = new Variable();
        Directory = new Variable();
        FileName = new Variable();
        FileNameNoExtension = new Variable();
        FileExtension = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FilePath);

        var fileInfo = new FileInfo(filePathValue);

        RootPath.Value = fileInfo.FullName;
        Directory.Value = fileInfo.Directory?.FullName;
        FileName.Value = fileInfo.Name;
        FileNameNoExtension.Value = fileInfo.Name.Replace(fileInfo.Extension, "");
        FileExtension.Value = fileInfo.Extension;

        sandBox.Variables.Add(RootPath);
        sandBox.Variables.Add(Directory);
        sandBox.Variables.Add(FileName);
        sandBox.Variables.Add(FileNameNoExtension);
        sandBox.Variables.Add(FileExtension);
    }
}