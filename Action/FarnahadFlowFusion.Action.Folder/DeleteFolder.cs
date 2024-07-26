using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Folder;

public class DeleteFolder : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Delete folder";

    public ActionInput FolderToDelete { get; set; }

    public DeleteFolder()
    {
        _cSharpService = new CSharpService();

        FolderToDelete = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {

        var folderToDeleteValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FolderToDelete);

        var directoryInfo = new DirectoryInfo(folderToDeleteValue);

        if (directoryInfo.Exists)
            directoryInfo.Delete();
    }
}