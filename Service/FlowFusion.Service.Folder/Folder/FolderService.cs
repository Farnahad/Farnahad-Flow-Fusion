using FlowFusion.Service.Folder.Folder.Base;

namespace FlowFusion.Service.Folder.Folder;

public class FolderService : IFolderService
{
    public async Task CopyFolder(string folderToCopy, string destinationFolder, IfFolderExists ifFolderExists)
    {
        var destinationFolderIsExists = Directory.Exists(destinationFolder);
        if ((destinationFolderIsExists && ifFolderExists == Base.IfFolderExists.Overwrite) ||
            destinationFolderIsExists == false)
        {
            await CopyFolderAsync(folderToCopy, destinationFolder);
        }
    }

    private static async Task CopyFolderAsync(string sourceFolder, string targetFolder)
    {
        var sourceDirectory = new DirectoryInfo(sourceFolder);
        var targetDirectory = new DirectoryInfo(targetFolder);

        if (sourceDirectory.Exists == false)
        {
            throw new DirectoryNotFoundException($"Source directory '{sourceFolder}' not found.");
        }

        if (targetDirectory.Exists == false)
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

    public string CreateFolder(string createNewFolderInto, string newFolderName)
    {
        var directoryInfo = new DirectoryInfo(Path.Combine(createNewFolderInto, newFolderName));

        if (directoryInfo.Exists == false)
            directoryInfo.Create();

        return directoryInfo.FullName;
    }

    public void DeleteFolder(string folderToDelete)
    {
        var directoryInfo = new DirectoryInfo(folderToDelete);

        if (directoryInfo.Exists)
            directoryInfo.Delete();
    }

    public void EmptyFolder(string folderToEmpty)
    {
        foreach (var file in Directory.GetFiles(folderToEmpty))
            global::System.IO.File.Delete(file);

        foreach (var directory in Directory.GetDirectories(folderToEmpty))
            Directory.Delete(directory, true);
    }

    public List<string> GetFilesInFolder(string folder, string fileFilter, bool includeSubfolders)
    {
        var searchPattern = string.IsNullOrWhiteSpace(fileFilter) ? "*.*" : fileFilter;
        var searchOption = includeSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        return [.. Directory.GetFiles(folder, searchPattern, searchOption)];
    }

    public string GetSpecialFolder(SpecialFolderName specialFolderName, string specialFolderPath)
    {
        var path = "";

        switch (specialFolderName)
        {
            case SpecialFolderName.ApplicationData:
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                break;
            case SpecialFolderName.CommonApplicationData:
                path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                break;
            case SpecialFolderName.CommonProgramFiles:
                path = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
                break;
            case SpecialFolderName.Cookies:
                path = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
                break;
            case SpecialFolderName.Desktop:
                path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                break;
            case SpecialFolderName.Documents:
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                break;
            case SpecialFolderName.Favorites:
                path = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
                break;
            case SpecialFolderName.History:
                path = Environment.GetFolderPath(Environment.SpecialFolder.History);
                break;
            case SpecialFolderName.InternetCache:
                path = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                break;
            case SpecialFolderName.LocalApplicationData:
                path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                break;
            case SpecialFolderName.Music:
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                break;
            case SpecialFolderName.Pictures:
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                break;
            case SpecialFolderName.ProgramFiles:
                path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                break;
            case SpecialFolderName.Programs:
                path = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
                break;
            case SpecialFolderName.Recent:
                path = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
                break;
            case SpecialFolderName.SendTo:
                path = Environment.GetFolderPath(Environment.SpecialFolder.SendTo);
                break;
            case SpecialFolderName.StartMenu:
                path = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
                break;
            case SpecialFolderName.Startup:
                path = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                break;
            case SpecialFolderName.System:
                path = Environment.GetFolderPath(Environment.SpecialFolder.System);
                break;
            case SpecialFolderName.Templates:
                path = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
                break;
        }

        if (string.IsNullOrEmpty(specialFolderPath) == false)
            path = specialFolderPath;

        return path;
    }

    public List<string> GetSubfoldersInFolder(string folder, string folderFilter, bool includeSubfolders)
    {
        var filter = string.IsNullOrWhiteSpace(folderFilter) == false ? folderFilter : "*";
        var searchOption = includeSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        return [.. Directory.GetDirectories(folder, filter, searchOption)];
    }

    public bool IfFolderExists(IfFolder ifFolder, string folderPath)
    {
        var directoryInfo = new DirectoryInfo(folderPath);

        return (directoryInfo.Exists && ifFolder == IfFolder.Exists) ||
               (directoryInfo.Exists == false && ifFolder == IfFolder.DosNotExist);
    }

    public string MoveFolder(string folderToMove, string destinationFolder)
    {
        var directoryInfo = new DirectoryInfo(folderToMove);
        directoryInfo.MoveTo(destinationFolder);

        return destinationFolder;
    }

    public string RenameFolder(string folderToRename, string newFolderName)
    {
        var directoryInfo = new DirectoryInfo(folderToRename);

        if (directoryInfo.Exists == false)
            directoryInfo.Create();

        directoryInfo.MoveTo(newFolderName);

        return newFolderName;
    }
}