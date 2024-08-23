using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Folder.Folder;

namespace FlowFusion.Action.Folder;

public class EmptyFolder(IFolderService folderService) : IAction
{
    public string Name => "Empty folder";

    public ActionInput FolderToEmpty { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var folderToEmpty = await sandBox.EvaluateActionInput<string>(FolderToEmpty);

        folderService.EmptyFolder(folderToEmpty);
    }
}