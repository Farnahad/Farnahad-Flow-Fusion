using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Folder;

public class RenameFolder : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Rename folder";

    public ActionInput FolderToRename { get; set; }
    public ActionInput NewFolderName { get; set; }
    public Variable RenamedFolder { get; set; }

    public RenameFolder()
    {
        _cSharpService = new CSharpService();

        FolderToRename = new ActionInput();
        NewFolderName = new ActionInput();
        RenamedFolder = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var folderToRenameValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FolderToRename);
        var newFolderNameValue = await _cSharpService.EvaluateActionInput<string>(sandBox, NewFolderName);

        var directoryInfo = new DirectoryInfo(folderToRenameValue);

        if (directoryInfo.Exists == false)
            directoryInfo.Create();

        directoryInfo.MoveTo(newFolderNameValue);
    }
}