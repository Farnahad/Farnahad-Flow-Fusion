using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.File.File;

namespace FlowFusion.Action.File;

public class ConvertFileToBinaryData(IFileService fileService) : IAction
{
    public string Name => "Convert file to binary data";

    public ActionInput FilePath { get; set; } = new();
    public Variable BinaryData { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

        BinaryData.Value = await fileService.ConvertFileToBinaryData(filePathValue);

        sandBox.SetVariable(BinaryData);
    }
}