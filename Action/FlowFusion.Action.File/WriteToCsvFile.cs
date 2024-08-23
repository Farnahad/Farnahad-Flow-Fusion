using System.Text;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.File.File;
using FlowFusion.Service.File.File.Base;

namespace FlowFusion.Action.File;

public class WriteToCsvFile(IFileService fileService) : IAction
{
    public string Name => "Write to csv file";

    public ActionInput VariableToWrite { get; set; } = new();
    public ActionInput FilePath { get; set; } = new();
    public WriteEncoding Encoding { get; set; } = WriteEncoding.Utf8;

    public async Task Execute(SandBox sandBox)
    {
        var variableToWriteValue = await sandBox.EvaluateActionInput<string>(VariableToWrite);
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

        await fileService.WriteToCsvFile(variableToWriteValue, filePathValue, Encoding);
    }
}