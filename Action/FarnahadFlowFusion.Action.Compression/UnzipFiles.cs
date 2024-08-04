using System.IO.Compression;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.Compression;

public class UnzipFiles : IAction
{
    public string Name => "Unzip files";

    public ActionInput ArchivePath { get; set; }
    public ActionInput DestinationFolder { get; set; }
    public ActionInput Password { get; set; }
    public ActionInput IncludeMask { get; set; }
    public ActionInput ExcludeMask { get; set; }

    public UnzipFiles()
    {
        ArchivePath = new ActionInput();
        DestinationFolder = new ActionInput();
        Password = new ActionInput();
        IncludeMask = new ActionInput();
        ExcludeMask = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var archivePathValue = await sandBox.EvaluateActionInput<string>(ArchivePath);
        var destinationFolderValue = await sandBox.EvaluateActionInput<string>(DestinationFolder);
        var passwordValue = await sandBox.EvaluateActionInput<string>(Password);
        var includeMaskValue = await sandBox.EvaluateActionInput<string>(IncludeMask);
        var excludeMaskValue = await sandBox.EvaluateActionInput<string>(ExcludeMask);

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