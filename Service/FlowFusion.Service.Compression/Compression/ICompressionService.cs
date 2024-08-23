using FlowFusion.Action.Compression.ZipFilesBase;

namespace FlowFusion.Service.Compression.Compression;

public interface ICompressionService
{
    void UnzipFiles(string archivePath, string destinationFolder, string password, string includeMask, string excludeMask);
    string ZipFiles(string archivePath, List<string> filesToZip,
        CompressionLevel compressionLevel, string password, string archiveComment);
}