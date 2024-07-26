using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.File;

public class DeleteFiles : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Delete file(s)";

    public ActionInput FilesToDelete { get; set; }

    public DeleteFiles()
    {
        _cSharpService = new CSharpService();

        FilesToDelete = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filesToDeleteValue = await _cSharpService.EvaluateActionInput<List<string>>(sandBox, FilesToDelete);

        foreach (var fileToDelete in filesToDeleteValue)
        {
            global::System.IO.File.Delete(fileToDelete);
        }
    }
}