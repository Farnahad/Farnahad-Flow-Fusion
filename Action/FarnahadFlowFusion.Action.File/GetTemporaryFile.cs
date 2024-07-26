using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.File;

public class GetTemporaryFile : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Get temporary file";

    public Variable TempFile { get; set; }

    public GetTemporaryFile()
    {
        _cSharpService = new CSharpService();

        TempFile = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var tempFilePath = Path.GetTempFileName();
        global::System.IO.File.Create(tempFilePath).Close();

        TempFile.Value = tempFilePath;

        sandBox.Variables.Add(TempFile);
        await Task.CompletedTask;
    }
}