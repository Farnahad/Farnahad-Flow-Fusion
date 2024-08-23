using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Folder.Folder;
using FlowFusion.Service.Folder.Folder.Base;

namespace FlowFusion.Action.Folder;

public class GetSpecialFolder(IFolderService folderService) : IAction
{
    public string Name => "Get special folder";

    public SpecialFolderName SpecialFolderName { get; set; } = SpecialFolderName.CommonProgramFiles;
    public ActionInput SpecialFolderPath { get; set; } = new();
    public Variable SpecialFolderPathVariable { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var specialFolderPathValue = await sandBox.EvaluateActionInput<string>(SpecialFolderPath);

        SpecialFolderPathVariable.Value = folderService.GetSpecialFolder(SpecialFolderName, specialFolderPathValue);

        sandBox.SetVariable(SpecialFolderPathVariable);
    }
}