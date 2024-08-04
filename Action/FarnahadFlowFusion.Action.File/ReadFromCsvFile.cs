using FarnahadFlowFusion.Action.File.ReadFromCsvFileBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.File;

public class ReadFromCsvFile : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Read from CSV file";

    public ActionInput FilePath { get; set; }
    public Encoding Encoding { get; set; }
    public Variable CsvTable { get; set; }

    public ReadFromCsvFile()
    {
        _cSharpService = new CSharpService();

        FilePath = new ActionInput();
        Encoding = Encoding.Utf8;
        CsvTable = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FilePath);

        var csvLines = new List<string[]>();

        var realEncoding = Encoding switch
        {
            Encoding.Ascii => global::System.Text.Encoding.ASCII,
            Encoding.SystemDefault => global::System.Text.Encoding.Default,
            Encoding.Unicode => global::System.Text.Encoding.Unicode,
            Encoding.UnicodeBigEndian => global::System.Text.Encoding.BigEndianUnicode,
            Encoding.Utf8 => global::System.Text.Encoding.UTF8,
            _ => global::System.Text.Encoding.Unicode
        };

        await using (var stream = new global::System.IO.FileStream(filePathValue, FileMode.Open, FileAccess.Read))
        using (var reader = new StreamReader(stream, realEncoding))
        {
            while (await reader.ReadLineAsync() is { } line)
            {
                Console.WriteLine(line);
            }
        }

        CsvTable.Value = csvLines;

        sandBox.Variables.Add(CsvTable);
    }
}