using System.Text;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;
using Encoding = FarnahadFlowFusion.Action.File.WriteToCsvFileBase.Encoding;

namespace FarnahadFlowFusion.Action.File;

public class WriteToCsvFile : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Write to csv file";

    public ActionInput VariableToWrite { get; set; }
    public ActionInput FilePath { get; set; }
    public Encoding Encoding { get; set; }

    public WriteToCsvFile()
    {
        _cSharpService = new CSharpService();

        VariableToWrite = new ActionInput();
        FilePath = new ActionInput();
        Encoding = Encoding.Utf8;
    }

    public async Task Execute(SandBox sandBox)
    {
        var variableToWriteValue = await _cSharpService.EvaluateActionInput<string>(sandBox, VariableToWrite);
        var filePathValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FilePath);

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