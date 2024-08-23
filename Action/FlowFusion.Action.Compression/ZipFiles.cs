using FlowFusion.Action.Compression.ZipFilesBase;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Compression.Compression;

namespace FlowFusion.Action.Compression;

public class ZipFiles(ICompressionService compressionService) : IAction
{
    public string Name => "Zip files";

    public ActionInput ArchivePath { get; set; } = new();
    public ActionInput FilesToZip { get; set; } = new();
    public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.BestBalanceOfSpeedAndCompression;
    public ActionInput Password { get; set; } = new();
    public ActionInput ArchiveComment { get; set; } = new();
    public Variable ZipFile { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var archivePathValue = await sandBox.EvaluateActionInput<string>(ArchivePath);
        var filesToZipValue = await sandBox.EvaluateActionInput<List<string>>(FilesToZip);
        var passwordValue = await sandBox.EvaluateActionInput<string>(Password);
        var archiveCommentValue = await sandBox.EvaluateActionInput<string>(ArchiveComment);

        ZipFile.Value = compressionService.ZipFiles(archivePathValue, filesToZipValue,
            CompressionLevel, passwordValue, archiveCommentValue);

        sandBox.SetVariable(ZipFile);
    }
}