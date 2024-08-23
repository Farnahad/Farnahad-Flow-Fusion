using FlowFusion.Service.File.File.Base;

namespace FlowFusion.Service.File.File;

public interface IFileService
{
    Task ConvertBase64ToFile(string base64EncodedText, string filePath, IfFileExists ifFileExists);
    Task ConvertBinaryDataToFile(byte[] binaryData, string filePath, IfFileExists ifFileExists);
    Task<string> ConvertFileToBase64(string filePath);
    Task<byte[]> ConvertFileToBinaryData(string filePath);
    List<string> CopyFiles(List<string> filesToCopy, string destinationFolder, IfFileExists ifFileExists);
    void DeleteFiles(List<string> filesToDelete);
    void GetFilePathPart(string filePath, out string rootPath, out string directory,
        out string fileName, out string fileNameNoExtension, out string fileExtension);
    string GetTemporaryFile();
    bool IfFileExists(IfFile ifFile, string filePath);
    List<string> MoveFiles(List<string> filesToMove, string destinationFolder, IfFileExists ifFileExists);
    Task<List<string[]>> ReadFromCsvFile(string filePath, ReadEncoding encoding);
    Task<object> ReadTextFromFile(string filePath, StoreContentAs storeContentAs, ReadEncoding encoding);
    void RenameFiles(string fileToRename, RenameSchema renameSchema, string newFileName,
        bool keepExtension, IfFileExists ifFileExists);
    Task WaiteForFile(WaiteForFileToBe waiteForFileToBe, string filePath,
        bool failWithTimeoutError, int duration);
    Task WriteTextToFile(string filePtah, string textToWrite, bool appendNewLine,
        WriteTextToFileIfFileExists ifFileExists, WriteEncoding encoding);
    Task WriteToCsvFile(string variableToWrite, string filePath, WriteEncoding encoding);
}