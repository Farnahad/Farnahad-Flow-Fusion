using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Folder.Folder;
using FlowFusion.Service.Folder.Folder.Base;

namespace FlowFusion.Action.Folder;

public class IfFolderExists(IFolderService folderService) : IAction
{
    public string Name => "If folder exists";

    public IfFolder IfFolder { get; set; } = IfFolder.Exists;
    public ActionInput FolderPath { get; set; } = new();

    // ReSharper disable once CollectionNeverUpdated.Global
    public List<IAction> Actions { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var folderPathValue = await sandBox.EvaluateActionInput<string>(FolderPath);

        if (folderService.IfFolderExists(IfFolder, folderPathValue))
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);
        }
    }
}