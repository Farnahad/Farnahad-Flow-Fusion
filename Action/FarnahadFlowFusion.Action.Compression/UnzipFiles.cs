using System.IO.Compression;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Compression;

public class UnzipFiles : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Unzip files";

    public ActionInput ArchivePath { get; set; }
    public ActionInput DestinationFolder { get; set; }
    public ActionInput Password { get; set; }
    public ActionInput IncludeMask { get; set; }
    public ActionInput ExcludeMask { get; set; }

    public UnzipFiles()
    {
        _cSharpService = new CSharpService();

        ArchivePath = new ActionInput();
        DestinationFolder = new ActionInput();
        Password = new ActionInput();
        IncludeMask = new ActionInput();
        ExcludeMask = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var archivePathValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ArchivePath);
        var destinationFolderValue = await _cSharpService.EvaluateActionInput<string>(sandBox, DestinationFolder);
        var passwordValue = await _cSharpService.EvaluateActionInput<string>(sandBox, Password);
        var includeMaskValue = await _cSharpService.EvaluateActionInput<string>(sandBox, IncludeMask);
        var excludeMaskValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ExcludeMask);

        using (var archive = ZipFile.Open(archivePathValue, ZipArchiveMode.Read))
        {
            foreach (var entry in archive.Entries)
            {
                if (string.IsNullOrWhiteSpace(includeMaskValue) == false &&
                    entry.FullName.Contains(includeMaskValue) == false)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(excludeMaskValue) == false &&
                    entry.FullName.Contains(excludeMaskValue))
                {
                    continue;
                }


                var destinationPath = Path.Combine(destinationFolderValue, entry.FullName);
                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath) ?? string.Empty);
                entry.ExtractToFile(destinationPath, true);
            }
        }
    }
}