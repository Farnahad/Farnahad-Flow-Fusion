using FarnahadFlowFusion.Action.File.RenameFilesBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.File;

public class RenameFiles : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Rename file(s)";

    public ActionInput FileToRename { get; set; }
    public RenameSchema RenameSchema { get; set; }
    public ActionInput NewFileName { get; set; }
    public bool KeepExtension { get; set; }
    public RenameFilesBase.IfFileExists IfFileExists { get; set; }

    public RenameFiles()
    {
        _cSharpService = new CSharpService();

        FileToRename = new ActionInput();
        RenameSchema = RenameSchema.SetNewName;
        NewFileName = new ActionInput();
        KeepExtension = true;
        IfFileExists = RenameFilesBase.IfFileExists.DoNothing;
    }

    public async Task Execute(SandBox sandBox)
    {
        var fileToRenameValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FileToRename);
        var newFileNameValue = await _cSharpService.EvaluateActionInput<string>(sandBox, NewFileName);

        var sourceFileInfo = new FileInfo(fileToRenameValue);

        if (sourceFileInfo.Directory != null)
        {
            var directory = sourceFileInfo.Directory.FullName;
            var fileNameWithoutExtension = sourceFileInfo.Name.Replace(sourceFileInfo.Extension, "");
            var extension = sourceFileInfo.Extension;
            var destinationFileName = "";

            switch (RenameSchema)
            {
                case RenameSchema.AddDatetime:
                    var now = global::System.DateTime.Now;
                    destinationFileName = $"{fileNameWithoutExtension}_{now:yyyyMMddHHmmss}{extension}";
                    break;
                case RenameSchema.AddText:
                    destinationFileName = $"{fileNameWithoutExtension}_{newFileNameValue}{extension}";
                    break;
                case RenameSchema.ChangeExtension:
                    destinationFileName = $"{fileNameWithoutExtension}.{newFileNameValue}";
                    break;
                case RenameSchema.MakeSequential:
                    destinationFileName = $"{fileNameWithoutExtension}_{newFileNameValue}{extension}";
                    break;
                case RenameSchema.RemoveText:
                    destinationFileName = fileNameWithoutExtension.Replace(newFileNameValue, "") + extension;
                    break;
                case RenameSchema.ReplaceText:
                    destinationFileName = fileNameWithoutExtension.Replace(newFileNameValue.Split('|')[0], newFileNameValue.Split('|')[1]) + extension;
                    break;
                case RenameSchema.SetNewName:
                    destinationFileName = newFileNameValue + extension;
                    break;
            }

            if (KeepExtension == false)
                destinationFileName = destinationFileName.Replace(extension, "");

            var destinationFileInfo = new FileInfo(Path.Combine(directory, destinationFileName));


            if ((destinationFileInfo.Exists && IfFileExists == RenameFilesBase.IfFileExists.Overwrite) ||
                destinationFileInfo.Exists == false)
            {
                sourceFileInfo.MoveTo(destinationFileInfo.FullName);
            }
        }
    }
}