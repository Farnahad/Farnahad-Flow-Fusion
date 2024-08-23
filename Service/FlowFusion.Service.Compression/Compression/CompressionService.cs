using System.IO.Compression;
using CompressionLevel = FlowFusion.Action.Compression.ZipFilesBase.CompressionLevel;

namespace FlowFusion.Service.Compression.Compression;

public class CompressionService : ICompressionService
{
    public void UnzipFiles(string archivePath, string destinationFolder, string password,
        string includeMask, string excludeMask)
    {
        using var archive = ZipFile.Open(archivePath, ZipArchiveMode.Read);
        foreach (var entry in archive.Entries)
        {
            if (string.IsNullOrWhiteSpace(includeMask) == false &&
                entry.FullName.Contains(includeMask) == false)
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(excludeMask) == false &&
                entry.FullName.Contains(excludeMask))
            {
                continue;
            }

            var destinationPath = Path.Combine(destinationFolder, entry.FullName);
            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath) ?? string.Empty);
            entry.ExtractToFile(destinationPath, true);
        }
    }

    public string ZipFiles(string archivePath, List<string> filesToZip, CompressionLevel compressionLevel,
        string password, string archiveComment)
    {
        using var archive = global::System.IO.Compression.ZipFile.Open(archivePath, ZipArchiveMode.Create);
        archive.Comment = archiveComment;
        // archive.Password = password;

        foreach (var fileToZip in filesToZip)
        {
            if (global::System.IO.File.Exists(fileToZip))
            {
                var entryName = Path.GetFileName(fileToZip);

                var targetCompressionLevel = global::System.IO.Compression.CompressionLevel.Optimal;

                switch (compressionLevel)
                {
                    case CompressionLevel.BestBalanceOfSpeedAndCompression:
                        targetCompressionLevel = global::System.IO.Compression.CompressionLevel.Optimal;
                        break;
                    case CompressionLevel.BestCompression:
                        targetCompressionLevel = global::System.IO.Compression.CompressionLevel.SmallestSize;
                        break;
                    case CompressionLevel.BestSpeed:
                        targetCompressionLevel = global::System.IO.Compression.CompressionLevel.Fastest;
                        break;
                    case CompressionLevel.None:
                        targetCompressionLevel = global::System.IO.Compression.CompressionLevel.NoCompression;
                        break;
                }

                var zipEntry = archive.CreateEntryFromFile(fileToZip, entryName, targetCompressionLevel);
            }
        }

        return archivePath;
    }
}