using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Folder;

public class CopyFolder : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Copy folder";

    public ActionInput FolderToCopy { get; set; }
    public ActionInput DestinationFolder { get; set; }
    public CopyFolderBase.IfFolderExists IfFolderExists { get; set; }
    public Variable CopiedFolder { get; set; }

    public CopyFolder()
    {
        _cSharpService = new CSharpService();

        FolderToCopy = new ActionInput();
        DestinationFolder = new ActionInput();
        IfFolderExists = CopyFolderBase.IfFolderExists.DoNothing;
        CopiedFolder = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var folderToCopyValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FolderToCopy);
        var destinationFolderValue = await _cSharpService.EvaluateActionInput<string>(sandBox, DestinationFolder);

        var destinationFolderIsExists = Directory.Exists(destinationFolderValue);
        if ((destinationFolderIsExists && IfFolderExists == CopyFolderBase.IfFolderExists.Overwrite) ||
            destinationFolderIsExists == false)
        {
            await CopyFolderAsync(folderToCopyValue, destinationFolderValue);
        }

        CopiedFolder.Value = destinationFolderValue;

        sandBox.Variables.Add(CopiedFolder);
    }

    private static async Task CopyFolderAsync(string sourceFolder, string targetFolder)
    {
        var sourceDirectory = new DirectoryInfo(sourceFolder);
        var targetDirectory = new DirectoryInfo(targetFolder);

        if (!sourceDirectory.Exists)
        {
            throw new DirectoryNotFoundException($"Source directory '{sourceFolder}' not found.");
        }

        if (!targetDirectory.Exists)
        {
            targetDirectory.Create();
            Console.WriteLine($"Target directory '{targetFolder}' created.");
        }

        foreach (var file in sourceDirectory.GetFiles())
        {
            var filePath = Path.Combine(targetFolder, file.Name);
            await CopyFileAsync(file.FullName, filePath);
            Console.WriteLine($"File '{file.Name}' copied to '{targetFolder}'.");
        }

        foreach (var subDirectory in sourceDirectory.GetDirectories())
        {
            var subDirectoryPath = Path.Combine(targetFolder, subDirectory.Name);
            await CopyFolderAsync(subDirectory.FullName, subDirectoryPath);
        }
    }

    private static async Task CopyFileAsync(string sourceFile, string destFile)
    {
        await using var sourceStream = new FileStream(sourceFile, FileMode.Open,
            FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
        await using var destStream = new FileStream(destFile, FileMode.CreateNew,
            FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
        await sourceStream.CopyToAsync(destStream);
    }
}