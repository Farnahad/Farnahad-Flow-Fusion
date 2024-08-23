using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.File.File;

namespace FlowFusion.Action.File;

public class ConvertBinaryDataToFile(IFileService fileService) : IAction
{
    public string Name => "Convert binary data to file";

    public ActionInput BinaryData { get; set; } = new();
    public ActionInput FilePath { get; set; } = new();
    public Service.File.File.Base.IfFileExists IfFileExists { get; set; } = Service.File.File.Base.IfFileExists.DoNothing;

    public async Task Execute(SandBox sandBox)
    {
        var binaryDataValue = await sandBox.EvaluateActionInput<byte[]>(BinaryData);
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

        await fileService.ConvertBinaryDataToFile(binaryDataValue, filePathValue, IfFileExists);
    }
}