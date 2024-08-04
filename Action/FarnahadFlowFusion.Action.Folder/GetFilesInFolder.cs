using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Folder;

public class GetFilesInFolder : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Get files in folder";

    public ActionInput Folder { get; set; }
    public ActionInput FileFilter { get; set; }
    public bool IncludeSubfolders { get; set; }
    public Variable Files { get; set; }

    public GetFilesInFolder()
    {
        _cSharpService = new CSharpService();

        Folder = new ActionInput();
        FileFilter = new ActionInput();
        IncludeSubfolders = false;
        Files = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var folderValue = await _cSharpService.EvaluateActionInput<string>(sandBox, Folder);
        var fileFilterValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FileFilter);

        var searchPattern = string.IsNullOrWhiteSpace(fileFilterValue) ? "*.*" : fileFilterValue;
        var searchOption = IncludeSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        Files.Value = new List<string>(Directory.GetFiles(folderValue, searchPattern, searchOption));

        sandBox.Variables.Add(Files);
    }
}