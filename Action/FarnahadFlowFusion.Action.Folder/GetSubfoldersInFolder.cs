using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Folder;

public class GetSubfoldersInFolder : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Get subfolders in folder";

    public ActionInput Folder { get; set; }
    public ActionInput FolderFilter { get; set; }
    public bool IncludeSubfolders { get; set; }
    public Variable Folders { get; set; }

    public GetSubfoldersInFolder()
    {
        _cSharpService = new CSharpService();

        Folder = new ActionInput();
        FolderFilter = new ActionInput();
        IncludeSubfolders = false;
        Folders = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var folderValue = await _cSharpService.EvaluateActionInput<string>(sandBox, Folder);
        var folderFilterValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FolderFilter);

        var filter = string.IsNullOrWhiteSpace(folderFilterValue) == false ? folderFilterValue : "*";
        var searchOption = IncludeSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        Folders.Value = new List<string>(Directory.GetDirectories(folderValue, filter, searchOption));

        sandBox.Variables.Add(Folders);
    }
}