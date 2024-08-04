using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.File;

public class ConvertFileToBinaryData : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Convert file to binary data";

    public ActionInput FilePath { get; set; }
    public Variable BinaryData { get; set; }

    public ConvertFileToBinaryData()
    {
        _cSharpService = new CSharpService();

        FilePath = new ActionInput();
        BinaryData = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FilePath);

        BinaryData.Value = await global::System.IO.File.ReadAllBytesAsync(filePathValue);

        sandBox.Variables.Add(BinaryData);
    }
}