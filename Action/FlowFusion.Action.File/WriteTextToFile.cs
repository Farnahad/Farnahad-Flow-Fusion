using System.Text;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.File.File;
using FlowFusion.Service.File.File.Base;

namespace FlowFusion.Action.File;

public class WriteTextToFile(IFileService fileService) : IAction
{
    public string Name => "Write text to file";

    public ActionInput FilePtah { get; set; } = new();
    public ActionInput TextToWrite { get; set; } = new();
    public bool AppendNewLine { get; set; } = true;
    public WriteTextToFileIfFileExists IfFileExists { get; set; } = WriteTextToFileIfFileExists.OverwriteExistingContent;
    public WriteEncoding Encoding { get; set; } = WriteEncoding.Unicode;

    public async Task Execute(SandBox sandBox)
    {
        var filePtahValue = await sandBox.EvaluateActionInput<string>(FilePtah);
        var textToWriteValue = await sandBox.EvaluateActionInput<string>(TextToWrite);

        await fileService.WriteTextToFile(textToWriteValue, textToWriteValue, AppendNewLine, IfFileExists, Encoding);
    }
}