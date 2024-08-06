using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.File;

public class DeleteFiles : IAction
{
    public string Name => "Delete file(s)";

    public ActionInput FilesToDelete { get; set; }

    public DeleteFiles()
    {
        FilesToDelete = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filesToDeleteValue = await sandBox.EvaluateActionInput<List<string>>(FilesToDelete);

        foreach (var fileToDelete in filesToDeleteValue)
        {
            global::System.IO.File.Delete(fileToDelete);
        }
    }
}