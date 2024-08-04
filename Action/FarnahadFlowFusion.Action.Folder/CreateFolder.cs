using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Folder;

public class CreateFolder : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Create folder";

    public ActionInput CreateNewFolderInto { get; set; }
    public ActionInput NewFolderName { get; set; }
    public Variable NewFolder { get; set; }

    public CreateFolder()
    {
        _cSharpService = new CSharpService();

        CreateNewFolderInto = new ActionInput();
        NewFolderName = new ActionInput();
        NewFolder = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var createNewFolderIntoValue = await _cSharpService.EvaluateActionInput<string>(sandBox, CreateNewFolderInto);
        var newFolderNameValue = await _cSharpService.EvaluateActionInput<string>(sandBox, NewFolderName);

        var directoryInfo = new DirectoryInfo(Path.Combine(createNewFolderIntoValue, newFolderNameValue));

        if (directoryInfo.Exists == false)
            directoryInfo.Create();

        NewFolder.Value = directoryInfo.FullName;

        sandBox.Variables.Add(NewFolder);
    }
}