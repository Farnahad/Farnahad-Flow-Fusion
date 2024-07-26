using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.File;

public class ConvertBase64ToFile : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Convert base64 to file";

    public ActionInput Base64EncodedText { get; set; }
    public ActionInput FilePath { get; set; }
    public ConvertBase64ToFileBase.IfFileExists IfFileExists { get; set; }

    public ConvertBase64ToFile()
    {
        _cSharpService = new CSharpService();

        Base64EncodedText = new ActionInput();
        FilePath = new ActionInput();
        IfFileExists = ConvertBase64ToFileBase.IfFileExists.DoNothing;
    }

    public async Task Execute(SandBox sandBox)
    {
        var base64EncodedTextValue = await _cSharpService.EvaluateActionInput<string>(sandBox, Base64EncodedText);
        var filePathValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FilePath);

        var fileExists = global::System.IO.File.Exists(filePathValue);

        if ((fileExists && IfFileExists == ConvertBase64ToFileBase.IfFileExists.Overwrite) || fileExists == false)
        {
            var fileBytes = Convert.FromBase64String(base64EncodedTextValue);
            // ReSharper disable once AssignNullToNotNullAttribute
            await global::System.IO.File.WriteAllBytesAsync(filePathValue, fileBytes);
        }
    }
}