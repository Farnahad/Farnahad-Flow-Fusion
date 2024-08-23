using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.File.File;
using FlowFusion.Service.File.File.Base;

namespace FlowFusion.Action.File;

public class ReadFromCsvFile(IFileService fileService) : IAction
{
    public string Name => "Read from CSV file";

    public ActionInput FilePath { get; set; } = new();
    public ReadEncoding Encoding { get; set; } = ReadEncoding.Utf8;
    public Variable CsvTable { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

        CsvTable.Value = await fileService.ReadFromCsvFile(filePathValue, Encoding);

        sandBox.SetVariable(CsvTable);
    }
}