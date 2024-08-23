using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.File.File;
using FlowFusion.Service.File.File.Base;

namespace FlowFusion.Action.File;

public class RenameFiles(IFileService fileService) : IAction
{
    public string Name => "Rename file(s)";

    public ActionInput FileToRename { get; set; } = new();
    public RenameSchema RenameSchema { get; set; } = RenameSchema.SetNewName;
    public ActionInput NewFileName { get; set; } = new();
    public bool KeepExtension { get; set; } = true;
    public Service.File.File.Base.IfFileExists IfFileExists { get; set; } = Service.File.File.Base.IfFileExists.DoNothing;

    public async Task Execute(SandBox sandBox)
    {
        var fileToRenameValue = await sandBox.EvaluateActionInput<string>(FileToRename);
        var newFileNameValue = await sandBox.EvaluateActionInput<string>(NewFileName);

        fileService.RenameFiles(newFileNameValue, RenameSchema, newFileNameValue, KeepExtension, IfFileExists);
    }
}