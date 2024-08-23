using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.File.File;
using FlowFusion.Service.File.File.Base;

namespace FlowFusion.Action.File;

public class ReadTextFromFile(IFileService fileService) : IAction
{
    public string Name => "Read text from file";

    public ActionInput FilePath { get; set; } = new();
    public StoreContentAs StoreContentAs { get; set; } = StoreContentAs.SingleTextValue;
    public ReadEncoding Encoding { get; set; } = ReadEncoding.Utf8;
    public Variable FileContents { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

        FileContents.Value = await fileService.ReadTextFromFile(filePathValue, StoreContentAs, Encoding);

        sandBox.SetVariable(FileContents);
    }
}