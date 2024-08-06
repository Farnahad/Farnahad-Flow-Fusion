using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Folder;

public class GetFilesInFolder : IAction //XXXXXXXXXXXX
{
    public string Name => "Get files in folder";

    public ActionInput Folder { get; set; }
    public ActionInput FileFilter { get; set; }
    public bool IncludeSubfolders { get; set; }
    public Variable Files { get; set; }

    public GetFilesInFolder()
    {
        Folder = new ActionInput();
        FileFilter = new ActionInput();
        IncludeSubfolders = false;
        Files = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var folderValue = await sandBox.EvaluateActionInput<string>(Folder);
        var fileFilterValue = await sandBox.EvaluateActionInput<string>(FileFilter);

        var searchPattern = string.IsNullOrWhiteSpace(fileFilterValue) ? "*.*" : fileFilterValue;
        var searchOption = IncludeSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        Files.Value = new List<string>(Directory.GetFiles(folderValue, searchPattern, searchOption));

        sandBox.SetVariable(Files);
    }
}