using System.IO.Compression;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;
using CompressionLevel = FarnahadFlowFusion.Action.Compression.ZipFilesBase.CompressionLevel;

namespace FarnahadFlowFusion.Action.Compression;

public class ZipFiles : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Zip files";

    public ActionInput ArchivePath { get; set; }
    public ActionInput FilesToZip { get; set; }
    public CompressionLevel CompressionLevel { get; set; }
    public ActionInput Password { get; set; }
    public ActionInput ArchiveComment { get; set; }
    public Variable ZipFile { get; set; }

    public ZipFiles()
    {
        _cSharpService = new CSharpService();

        ArchivePath = new ActionInput();
        FilesToZip = new ActionInput();
        CompressionLevel = CompressionLevel.BestBalanceOfSpeedAndCompression;
        Password = new ActionInput();
        ArchiveComment = new ActionInput();
        ZipFile = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var archivePathValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ArchivePath);
        var filesToZipValue = await _cSharpService.EvaluateActionInput<List<string>>(sandBox, FilesToZip);
        var passwordValue = await _cSharpService.EvaluateActionInput<string>(sandBox, Password);
        var archiveCommentValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ArchiveComment);

        using (var archive = global::System.IO.Compression.ZipFile.Open(archivePathValue, ZipArchiveMode.Create))
        {
            archive.Comment = archiveCommentValue;
            // archive.Password = passwordValue;

            foreach (var fileToZip in filesToZipValue)
            {
                if (global::System.IO.File.Exists(fileToZip))
                {
                    var entryName = Path.GetFileName(fileToZip);

                    var targetCompressionLevel = global::System.IO.Compression.CompressionLevel.Optimal;

                    switch (CompressionLevel)
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
        }

        ZipFile.Value = archivePathValue;

        sandBox.Variables.Add(ZipFile);
    }
}