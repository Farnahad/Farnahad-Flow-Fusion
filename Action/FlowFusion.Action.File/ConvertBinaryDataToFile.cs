using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.File;

public class ConvertBinaryDataToFile : IAction
{
    public string Name => "Convert binary data to file";

    public ActionInput BinaryData { get; set; }
    public ActionInput FilePath { get; set; }
    public ConvertBinaryDataToFileBase.IfFileExists IfFileExists { get; set; }

    public ConvertBinaryDataToFile()
    {
        BinaryData = new ActionInput();
        FilePath = new ActionInput();
        IfFileExists = ConvertBinaryDataToFileBase.IfFileExists.DoNothing;
    }

    public async Task Execute(SandBox sandBox)
    {
        var binaryDataValue = await sandBox.EvaluateActionInput<byte[]>(BinaryData);
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

        var fileExists = global::System.IO.File.Exists(filePathValue);

        if ((fileExists && IfFileExists == ConvertBinaryDataToFileBase.IfFileExists.Overwrite) || fileExists == false)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            await global::System.IO.File.WriteAllBytesAsync(filePathValue, binaryDataValue);
        }
    }
}