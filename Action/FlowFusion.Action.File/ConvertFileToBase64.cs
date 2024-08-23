using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.File.File;

namespace FlowFusion.Action.File;

public class ConvertFileToBase64(IFileService fileService) : IAction
{
    public string Name => "Convert file to Base64";

    public ActionInput FilePath { get; set; } = new();
    public Variable Base64Text { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

        Base64Text.Value = fileService.ConvertFileToBase64(filePathValue);

        sandBox.SetVariable(Base64Text);
    }
}