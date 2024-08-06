using System.Text;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using Encoding = FlowFusion.Action.File.WriteToCsvFileBase.Encoding;

namespace FlowFusion.Action.File;

public class WriteToCsvFile : IAction //XXXXXXXXXXXX
{
    public string Name => "Write to csv file";

    public ActionInput VariableToWrite { get; set; }
    public ActionInput FilePath { get; set; }
    public Encoding Encoding { get; set; }

    public WriteToCsvFile()
    {
        VariableToWrite = new ActionInput();
        FilePath = new ActionInput();
        Encoding = Encoding.Utf8;
    }

    public async Task Execute(SandBox sandBox)
    {
        var variableToWriteValue = await sandBox.EvaluateActionInput<string>(VariableToWrite);
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

        var encoding = Encoding switch
        {
            Encoding.Ascii => global::System.Text.Encoding.ASCII,
            Encoding.SystemDefault => global::System.Text.Encoding.Default,
            Encoding.Unicode => global::System.Text.Encoding.Unicode,
            Encoding.UnicodeBigEndian => global::System.Text.Encoding.BigEndianUnicode,
            Encoding.UnicodeNoByteOrderMark => new UnicodeEncoding(false, true),
            Encoding.Utf8 => global::System.Text.Encoding.UTF8,
            Encoding.Utf8NoByteOrderMark => new UTF8Encoding(false),
            _ => global::System.Text.Encoding.UTF8
        };

        await using var writer = new StreamWriter(filePathValue, false, encoding);
        await writer.WriteLineAsync(variableToWriteValue);
    }
}