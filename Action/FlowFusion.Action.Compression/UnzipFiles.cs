using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Compression.Compression;

namespace FlowFusion.Action.Compression;

public class UnzipFiles(ICompressionService compressionService) : IAction
{
    private readonly ICompressionService _compressionService = compressionService;
    public string Name => "Unzip files";

    public ActionInput ArchivePath { get; set; } = new();
    public ActionInput DestinationFolder { get; set; } = new();
    public ActionInput Password { get; set; } = new();
    public ActionInput IncludeMask { get; set; } = new();
    public ActionInput ExcludeMask { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var archivePathValue = await sandBox.EvaluateActionInput<string>(ArchivePath);
        var destinationFolderValue = await sandBox.EvaluateActionInput<string>(DestinationFolder);
        var passwordValue = await sandBox.EvaluateActionInput<string>(Password);
        var includeMaskValue = await sandBox.EvaluateActionInput<string>(IncludeMask);
        var excludeMaskValue = await sandBox.EvaluateActionInput<string>(ExcludeMask);

        compressionService.UnzipFiles(archivePathValue, destinationFolderValue,
            passwordValue, includeMaskValue, excludeMaskValue);
    }
}