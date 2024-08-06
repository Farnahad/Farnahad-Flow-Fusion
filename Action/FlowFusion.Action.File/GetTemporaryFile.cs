using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.File;

public class GetTemporaryFile : IAction //XXXXXXXXXXXX
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