using FarnahadFlowFusion.Action.Folder.IfFolderExistsBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Folder;

public class IfFolderExists : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "If folder exists";

    public IfFolder IfFolder { get; set; }
    public ActionInput FolderPath { get; set; }
    public List<IAction> Actions { get; set; }

    public IfFolderExists()
    {
        _cSharpService = new CSharpService();

        IfFolder = IfFolder.Exists;
        FolderPath = new ActionInput();
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        var folderPathValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FolderPath);

        var directoryInfo = new DirectoryInfo(folderPathValue);

        if ((directoryInfo.Exists && IfFolder == IfFolder.Exists) ||
            (directoryInfo.Exists == false && IfFolder == IfFolder.DosNotExist))
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);
        }
    }
}