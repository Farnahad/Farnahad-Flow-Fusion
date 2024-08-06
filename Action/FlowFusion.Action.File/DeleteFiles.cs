using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.File;

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