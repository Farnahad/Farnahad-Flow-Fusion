using FlowFusion.Action.Folder.IfFolderExistsBase;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Folder;

public class IfFolderExists : IAction
{
    public string Name => "If folder exists";

    public IfFolder IfFolder { get; set; }
    public ActionInput FolderPath { get; set; }
    public List<IAction> Actions { get; set; }

    public IfFolderExists()
    {
        IfFolder = IfFolder.Exists;
        FolderPath = new ActionInput();
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        var folderPathValue = await sandBox.EvaluateActionInput<string>(FolderPath);

        var directoryInfo = new DirectoryInfo(folderPathValue);

        if ((directoryInfo.Exists && IfFolder == IfFolder.Exists) ||
            (directoryInfo.Exists == false && IfFolder == IfFolder.DosNotExist))
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);
        }
    }
}