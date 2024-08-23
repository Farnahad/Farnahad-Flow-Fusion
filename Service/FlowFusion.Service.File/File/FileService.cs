using System.Text;
using FlowFusion.Service.File.File.Base;

namespace FlowFusion.Service.File.File;

public class FileService : IFileService
{
    public async Task ConvertBase64ToFile(string base64EncodedText, string filePath, IfFileExists ifFileExists)
    {
        var fileExists = global::System.IO.File.Exists(filePath);

        if ((fileExists && ifFileExists == Base.IfFileExists.Overwrite) || fileExists == false)
        {
            var fileBytes = Convert.FromBase64String(base64EncodedText);
            // ReSharper disable once AssignNullToNotNullAttribute
            await global::System.IO.File.WriteAllBytesAsync(filePath, fileBytes);
        }
    }

    public async Task ConvertBinaryDataToFile(byte[] binaryData, string filePath, IfFileExists ifFileExists)
    {
        var fileExists = global::System.IO.File.Exists(filePath);

        if ((fileExists && ifFileExists == Base.IfFileExists.Overwrite) || fileExists == false)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            await global::System.IO.File.WriteAllBytesAsync(filePath, binaryData);
        }
    }

    public async Task<string> ConvertFileToBase64(string filePath)
    {
        return Convert.ToBase64String(await global::System.IO.File.ReadAllBytesAsync(filePath));
    }

    public async Task<byte[]> ConvertFileToBinaryData(string filePath)
    {
        return await global::System.IO.File.ReadAllBytesAsync(filePath);
    }

    public List<string> CopyFiles(List<string> filesToCopy, string destinationFolder, IfFileExists ifFileExists)
    {
        var copiedFiles = new List<string>();

        foreach (var fileToCopy in filesToCopy)
        {
            var fileInfo = new FileInfo(fileToCopy);

            if ((fileInfo.Exists && ifFileExists == Base.IfFileExists.Overwrite) ||
                fileInfo.Exists == false)
            {
                fileInfo.CopyTo(destinationFolder);
                copiedFiles.Add(Path.Combine(destinationFolder, fileInfo.Name));
            }
        }

        return copiedFiles;
    }

    public void DeleteFiles(List<string> filesToDelete)
    {
        foreach (var fileToDelete in filesToDelete)
        {
            global::System.IO.File.Delete(fileToDelete);
        }
    }

    public void GetFilePathPart(string filePath, out string rootPath, out string directory, out string fileName,
        out string fileNameNoExtension, out string fileExtension)
    {
        var fileInfo = new FileInfo(filePath);

        rootPath = fileInfo.FullName;
        directory = fileInfo.Directory?.FullName;
        fileName = fileInfo.Name;
        fileNameNoExtension = fileInfo.Name.Replace(fileInfo.Extension, "");
        fileExtension = fileInfo.Extension;
    }

    public string GetTemporaryFile()
    {
        var tempFilePath = Path.GetTempFileName();
        global::System.IO.File.Create(tempFilePath).Close();

        return tempFilePath;
    }

    public bool IfFileExists(IfFile ifFile, string filePath)
    {
        var result = false;

        if (ifFile == IfFile.DoesNotExist)
        {
            result = global::System.IO.File.Exists(filePath);
        }
        else if (ifFile == IfFile.Exists)
        {
            result = global::System.IO.File.Exists(filePath) == false;
        }

        return result;
    }

    public List<string> MoveFiles(List<string> filesToMove, string destinationFolder, IfFileExists ifFileExists)
    {
        var movedFiles = new List<string>();

        foreach (var fileToMove in filesToMove)
        {
            var fileInfo = new FileInfo(fileToMove);

            if ((fileInfo.Exists && ifFileExists == Base.IfFileExists.Overwrite) || fileInfo.Exists == false)
            {
                fileInfo.MoveTo(destinationFolder);
                movedFiles.Add(Path.Combine(destinationFolder, fileInfo.Name));
            }
        }

        return movedFiles;
    }

    public async Task<List<string[]>> ReadFromCsvFile(string filePath, ReadEncoding encoding)
    {
        var csvLines = new List<string[]>();

        var systemEncoding = encoding switch
        {
            ReadEncoding.Ascii => global::System.Text.Encoding.ASCII,
            ReadEncoding.SystemDefault => global::System.Text.Encoding.Default,
            ReadEncoding.Unicode => global::System.Text.Encoding.Unicode,
            ReadEncoding.UnicodeBigEndian => global::System.Text.Encoding.BigEndianUnicode,
            ReadEncoding.Utf8 => global::System.Text.Encoding.UTF8,
            _ => global::System.Text.Encoding.Unicode
        };

        await using var stream = new global::System.IO.FileStream(filePath, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(stream, systemEncoding);

        while (await reader.ReadLineAsync() is { } line)
        {
            Console.WriteLine(line);
        }

        return csvLines;
    }

    public async Task<object> ReadTextFromFile(string filePath, StoreContentAs storeContentAs, ReadEncoding encoding)
    {
        var systemEncoding = encoding switch
        {
            ReadEncoding.Ascii => global::System.Text.Encoding.ASCII,
            ReadEncoding.SystemDefault => global::System.Text.Encoding.Default,
            ReadEncoding.Unicode => global::System.Text.Encoding.Unicode,
            ReadEncoding.UnicodeBigEndian => global::System.Text.Encoding.BigEndianUnicode,
            ReadEncoding.Utf8 => global::System.Text.Encoding.UTF8,
            _ => global::System.Text.Encoding.Unicode
        };

        var text = systemEncoding.GetString(await global::System.IO.File.ReadAllBytesAsync(filePath));

        return storeContentAs switch
        {
            StoreContentAs.ListEachIsAListItem => text.Split(Environment.NewLine),
            StoreContentAs.SingleTextValue => text,
            _ => null
        };
    }

    public void RenameFiles(string fileToRename, RenameSchema renameSchema, string newFileName, bool keepExtension,
        IfFileExists ifFileExists)
    {
        var sourceFileInfo = new FileInfo(fileToRename);

        if (sourceFileInfo.Directory != null)
        {
            var directory = sourceFileInfo.Directory.FullName;
            var fileNameWithoutExtension = sourceFileInfo.Name.Replace(sourceFileInfo.Extension, "");
            var extension = sourceFileInfo.Extension;
            var destinationFileName = "";

            switch (renameSchema)
            {
                case RenameSchema.AddDatetime:
                    var now = global::System.DateTime.Now;
                    destinationFileName = $"{fileNameWithoutExtension}_{now:yyyyMMddHHmmss}{extension}";
                    break;
                case RenameSchema.AddText:
                    destinationFileName = $"{fileNameWithoutExtension}_{newFileName}{extension}";
                    break;
                case RenameSchema.ChangeExtension:
                    destinationFileName = $"{fileNameWithoutExtension}.{newFileName}";
                    break;
                case RenameSchema.MakeSequential:
                    destinationFileName = $"{fileNameWithoutExtension}_{newFileName}{extension}";
                    break;
                case RenameSchema.RemoveText:
                    destinationFileName = fileNameWithoutExtension.Replace(newFileName, "") + extension;
                    break;
                case RenameSchema.ReplaceText:
                    destinationFileName = fileNameWithoutExtension.Replace(newFileName.Split('|')[0], newFileName.Split('|')[1]) + extension;
                    break;
                case RenameSchema.SetNewName:
                    destinationFileName = newFileName + extension;
                    break;
            }

            if (keepExtension == false)
                destinationFileName = destinationFileName.Replace(extension, "");

            var destinationFileInfo = new FileInfo(Path.Combine(directory, destinationFileName));


            if ((destinationFileInfo.Exists && ifFileExists == Base.IfFileExists.Overwrite) ||
                destinationFileInfo.Exists == false)
            {
                sourceFileInfo.MoveTo(destinationFileInfo.FullName);
            }
        }
    }

    public async Task WaiteForFile(WaiteForFileToBe waiteForFileToBe, string filePath, bool failWithTimeoutError, int duration)
    {
        var timeout = TimeSpan.FromMilliseconds(duration);

        using var cancellationTokenSource = new CancellationTokenSource(timeout);
        var fileWatchTask = waiteForFileToBe == WaiteForFileToBe.Created ?
            WaitForFileCreationAsync(filePath, cancellationTokenSource.Token) :
            WaitForFileDeletionAsync(filePath, cancellationTokenSource.Token);

        await fileWatchTask;
    }

    private Task WaitForFileCreationAsync(string filePath, CancellationToken token)
    {
        return Task.Run(async () =>
        {
            while (!global::System.IO.File.Exists(filePath))
            {
                token.ThrowIfCancellationRequested();
                await Task.Delay(100, token);
            }
        }, token);
    }

    private Task WaitForFileDeletionAsync(string filePath, CancellationToken token)
    {
        return Task.Run(async () =>
        {
            while (global::System.IO.File.Exists(filePath))
            {
                token.ThrowIfCancellationRequested();
                await Task.Delay(100, token);
            }
        }, token);
    }

    public async Task WriteTextToFile(string filePtah, string textToWrite, bool appendNewLine,
        WriteTextToFileIfFileExists ifFileExists, WriteEncoding encoding)
    {
        var systemEncoding = encoding switch
        {
            WriteEncoding.Ascii => global::System.Text.Encoding.ASCII,
            WriteEncoding.SystemDefault => global::System.Text.Encoding.Default,
            WriteEncoding.Unicode => global::System.Text.Encoding.Unicode,
            WriteEncoding.UnicodeBigEndian => global::System.Text.Encoding.BigEndianUnicode,
            WriteEncoding.UnicodeNoByteOrderMark => new UnicodeEncoding(false, true),
            WriteEncoding.Utf8 => global::System.Text.Encoding.UTF8,
            WriteEncoding.Utf8NoByteOrderMark => new UTF8Encoding(false),
            _ => global::System.Text.Encoding.UTF8
        };

        var fileInfo = new FileInfo(filePtah);

        if (appendNewLine)
            textToWrite = textToWrite + Environment.NewLine;

        if ((fileInfo.Exists && ifFileExists == WriteTextToFileIfFileExists.OverwriteExistingContent) ||
            fileInfo.Exists == false)
        {
            await global::System.IO.File.WriteAllTextAsync(filePtah, textToWrite, systemEncoding);
        }
        else if (fileInfo.Exists && ifFileExists == WriteTextToFileIfFileExists.AppendContent)
        {
            await global::System.IO.File.AppendAllTextAsync(filePtah, textToWrite, systemEncoding);
        }
    }

    public async Task WriteToCsvFile(string variableToWrite, string filePath, WriteEncoding encoding)
    {
        var systemEncoding = encoding switch
        {
            WriteEncoding.Ascii => global::System.Text.Encoding.ASCII,
            WriteEncoding.SystemDefault => global::System.Text.Encoding.Default,
            WriteEncoding.Unicode => global::System.Text.Encoding.Unicode,
            WriteEncoding.UnicodeBigEndian => global::System.Text.Encoding.BigEndianUnicode,
            WriteEncoding.UnicodeNoByteOrderMark => new UnicodeEncoding(false, true),
            WriteEncoding.Utf8 => global::System.Text.Encoding.UTF8,
            WriteEncoding.Utf8NoByteOrderMark => new UTF8Encoding(false),
            _ => global::System.Text.Encoding.UTF8
        };

        await using var writer = new StreamWriter(filePath, false, systemEncoding);
        await writer.WriteLineAsync(variableToWrite);
    }
}