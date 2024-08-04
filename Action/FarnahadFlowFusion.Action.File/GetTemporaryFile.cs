using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.File;

public class GetTemporaryFile : IAction
{
    public string Name => "Get temporary file";

    public Variable TempFile { get; set; }

    public GetTemporaryFile()
    {
        TempFile = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var tempFilePath = Path.GetTempFileName();
        global::System.IO.File.Create(tempFilePath).Close();

        TempFile.Value = tempFilePath;

        sandBox.SetVariable(TempFile);
        await Task.CompletedTask;
    }
}