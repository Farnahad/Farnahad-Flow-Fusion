using FlowFusion.Action.File.ReadFromCsvFileBase;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.File;

public class ReadFromCsvFile : IAction
{
    public string Name => "Read from CSV file";

    public ActionInput FilePath { get; set; }
    public Encoding Encoding { get; set; }
    public Variable CsvTable { get; set; }

    public ReadFromCsvFile()
    {
        FilePath = new ActionInput();
        Encoding = Encoding.Utf8;
        CsvTable = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

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

        sandBox.SetVariable(CsvTable);
    }
}