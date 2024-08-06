using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Folder;

public class EmptyFolder : IAction
{
    public string Name => "Empty folder";

    public ActionInput FolderToEmpty { get; set; }

    public EmptyFolder()
    {
        FolderToEmpty = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var folderToEmpty = await sandBox.EvaluateActionInput<string>(FolderToEmpty);

        foreach (var file in Directory.GetFiles(folderToEmpty))
            global::System.IO.File.Delete(file);

        foreach (var directory in Directory.GetDirectories(folderToEmpty))
            Directory.Delete(directory, true);
    }
}