using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Folder.Folder;

namespace FlowFusion.Action.Folder;

public class CopyFolder(IFolderService folderService) : IAction
{
    public string Name => "Copy folder";

    public ActionInput FolderToCopy { get; set; } = new();
    public ActionInput DestinationFolder { get; set; } = new();
    public Service.Folder.Folder.Base.IfFolderExists IfFolderExists { get; set; } = Service.Folder.Folder.Base.IfFolderExists.DoNothing;
    public Variable CopiedFolder { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var folderToCopyValue = await sandBox.EvaluateActionInput<string>(FolderToCopy);
        var destinationFolderValue = await sandBox.EvaluateActionInput<string>(DestinationFolder);

        await folderService.CopyFolder(folderToCopyValue, destinationFolderValue, IfFolderExists);

        CopiedFolder.Value = destinationFolderValue;

        sandBox.SetVariable(CopiedFolder);
    }
}