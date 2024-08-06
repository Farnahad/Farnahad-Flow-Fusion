using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.File;

public class ConvertBase64ToFile : IAction //XXXXXXXXXXXX
{
    public string Name => "Convert base64 to file";

    public ActionInput Base64EncodedText { get; set; }
    public ActionInput FilePath { get; set; }
    public ConvertBase64ToFileBase.IfFileExists IfFileExists { get; set; }

    public ConvertBase64ToFile()
    {
        Base64EncodedText = new ActionInput();
        FilePath = new ActionInput();
        IfFileExists = ConvertBase64ToFileBase.IfFileExists.DoNothing;
    }

    public async Task Execute(SandBox sandBox)
    {
        var base64EncodedTextValue = await sandBox.EvaluateActionInput<string>(Base64EncodedText);
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

        var fileExists = global::System.IO.File.Exists(filePathValue);

        if ((fileExists && IfFileExists == ConvertBase64ToFileBase.IfFileExists.Overwrite) || fileExists == false)
        {
            var fileBytes = Convert.FromBase64String(base64EncodedTextValue);
            // ReSharper disable once AssignNullToNotNullAttribute
            await global::System.IO.File.WriteAllBytesAsync(filePathValue, fileBytes);
        }
    }
}