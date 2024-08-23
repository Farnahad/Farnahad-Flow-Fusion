using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.File.File;

namespace FlowFusion.Action.File;

public class ConvertBase64ToFile(IFileService fileService) : IAction
{
    public string Name => "Convert base64 to file";

    public ActionInput Base64EncodedText { get; set; } = new();
    public ActionInput FilePath { get; set; } = new();
    public Service.File.File.Base.IfFileExists IfFileExists { get; set; } = Service.File.File.Base.IfFileExists.DoNothing;

    public async Task Execute(SandBox sandBox)
    {
        var base64EncodedTextValue = await sandBox.EvaluateActionInput<string>(Base64EncodedText);
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

        await fileService.ConvertBase64ToFile(filePathValue, filePathValue, IfFileExists);
    }
}