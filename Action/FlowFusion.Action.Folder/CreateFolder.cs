using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Folder.Folder;

namespace FlowFusion.Action.Folder;

public class CreateFolder(IFolderService folderService) : IAction
{
    public string Name => "Create folder";

    public ActionInput CreateNewFolderInto { get; set; } = new();
    public ActionInput NewFolderName { get; set; } = new();
    public Variable NewFolder { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var createNewFolderIntoValue = await sandBox.EvaluateActionInput<string>(CreateNewFolderInto);
        var newFolderNameValue = await sandBox.EvaluateActionInput<string>(NewFolderName);

        NewFolder.Value = folderService.CreateFolder(newFolderNameValue, newFolderNameValue);

        sandBox.SetVariable(NewFolder);
    }
}