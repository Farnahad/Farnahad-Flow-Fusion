using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.File;

public class ConvertFileToBase64 : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Convert file to Base64";

    public ActionInput FilePath { get; set; }
    public Variable Base64Text { get; set; }

    public ConvertFileToBase64()
    {
        _cSharpService = new CSharpService();

        FilePath = new ActionInput();
        Base64Text = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FilePath);
        Base64Text.Value = Convert.ToBase64String(await global::System.IO.File.ReadAllBytesAsync(filePathValue));

        sandBox.Variables.Add(Base64Text);
    }
}