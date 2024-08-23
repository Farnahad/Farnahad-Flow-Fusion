using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.File.File;
using FlowFusion.Service.File.File.Base;

namespace FlowFusion.Action.File;

public class IfFileExists(IFileService fileService) : IAction
{
    public string Name => "If File Exists";

    public IfFile IfFile { get; set; } = IfFile.Exists;
    public ActionInput FilePath { get; set; } = new();
    public List<IAction> Actions { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var filePath = await sandBox.EvaluateActionInput<string>(FilePath);

        if (fileService.IfFileExists(IfFile, filePath))
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);
        }
    }
}