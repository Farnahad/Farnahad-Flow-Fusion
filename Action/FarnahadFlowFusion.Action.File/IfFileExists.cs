using FarnahadFlowFusion.Action.File.IfFileExistsBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.File;

public class IfFileExists : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "If File Exists";

    public IfFile IfFile { get; set; }
    public ActionInput FilePath { get; set; }
    public List<IAction> Actions { get; set; }

    public IfFileExists()
    {
        _cSharpService = new CSharpService();

        IfFile = IfFile.Exists;
        FilePath = new ActionInput();
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filePath = await _cSharpService.EvaluateActionInput<string>(sandBox, FilePath);

        var result = false;

        if (IfFile == IfFile.DoesNotExist)
        {
            result = global::System.IO.File.Exists(filePath);
        }
        else if (IfFile == IfFile.Exists)
        {
            result = global::System.IO.File.Exists(filePath) == false;
        }

        if (result)
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);
        }
    }
}