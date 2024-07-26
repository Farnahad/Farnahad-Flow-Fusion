using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Folder;

public class EmptyFolder : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Empty folder";

    public ActionInput FolderToEmpty { get; set; }

    public EmptyFolder()
    {
        _cSharpService = new CSharpService();

        FolderToEmpty = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var folderToEmpty = await _cSharpService.EvaluateActionInput<string>(sandBox, FolderToEmpty);

        foreach (var file in Directory.GetFiles(folderToEmpty))
            global::System.IO.File.Delete(file);

        foreach (var directory in Directory.GetDirectories(folderToEmpty))
            Directory.Delete(directory, true);
    }
}