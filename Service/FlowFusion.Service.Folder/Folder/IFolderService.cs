using FlowFusion.Service.Folder.Folder.Base;

namespace FlowFusion.Service.Folder.Folder;

public interface IFolderService
{
    Task CopyFolder(string folderToCopy, string destinationFolder, IfFolderExists ifFolderExists);
    string CreateFolder(string createNewFolderInto, string newFolderName);
    void DeleteFolder(string folderToDelete);
    void EmptyFolder(string folderToEmpty);
    List<string> GetFilesInFolder(string folder, string fileFilter, bool includeSubfolders);
    string GetSpecialFolder(SpecialFolderName specialFolderName, string specialFolderPath);
    List<string> GetSubfoldersInFolder(string folder, string folderFilter, bool includeSubfolders);
    public bool IfFolderExists(IfFolder ifFolder, string folderPath);
    string MoveFolder(string folderToMove, string destinationFolder);
    string RenameFolder(string folderToRename, string newFolderName);
}