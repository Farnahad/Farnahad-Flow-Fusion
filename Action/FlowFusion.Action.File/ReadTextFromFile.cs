using FlowFusion.Action.File.ReadTextFromFileBase;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.File;

public class ReadTextFromFile : IAction
{
    public string Name => "Read text from file";

    public ActionInput FilePath { get; set; }
    public StoreContentAs StoreContentAs { get; set; }
    public Encoding Encoding { get; set; }
    public Variable FileContents { get; set; }

    public ReadTextFromFile()
    {
        FilePath = new ActionInput();
        StoreContentAs = StoreContentAs.SingleTextValue;
        Encoding = Encoding.Utf8;
        FileContents = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

        var realEncoding = Encoding switch
        {
            Encoding.Ascii => global::System.Text.Encoding.ASCII,
            Encoding.SystemDefault => global::System.Text.Encoding.Default,
            Encoding.Unicode => global::System.Text.Encoding.Unicode,
            Encoding.UnicodeBigEndian => global::System.Text.Encoding.BigEndianUnicode,
            Encoding.Utf8 => global::System.Text.Encoding.UTF8,
            _ => global::System.Text.Encoding.Unicode
        };

        var text = realEncoding.GetString(await global::System.IO.File.ReadAllBytesAsync(filePathValue));

        FileContents.Value = StoreContentAs switch
        {
            StoreContentAs.ListEachIsAListItem => text.Split(Environment.NewLine),
            StoreContentAs.SingleTextValue => text,
            _ => FileContents.Value
        };

        sandBox.SetVariable(FileContents);
    }
}