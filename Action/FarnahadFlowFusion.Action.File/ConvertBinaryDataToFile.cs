using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.File;

public class ConvertBinaryDataToFile : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Convert binary data to file";

    public ActionInput BinaryData { get; set; }
    public ActionInput FilePath { get; set; }
    public ConvertBinaryDataToFileBase.IfFileExists IfFileExists { get; set; }

    public ConvertBinaryDataToFile()
    {
        _cSharpService = new CSharpService();

        BinaryData = new ActionInput();
        FilePath = new ActionInput();
        IfFileExists = ConvertBinaryDataToFileBase.IfFileExists.DoNothing;
    }

    public async Task Execute(SandBox sandBox)
    {
        var binaryDataValue = await _cSharpService.EvaluateActionInput<byte[]>(sandBox, BinaryData);
        var filePathValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FilePath);

        var fileExists = global::System.IO.File.Exists(filePathValue);

        if ((fileExists && IfFileExists == ConvertBinaryDataToFileBase.IfFileExists.Overwrite) || fileExists == false)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            await global::System.IO.File.WriteAllBytesAsync(filePathValue, binaryDataValue);
        }
    }
}