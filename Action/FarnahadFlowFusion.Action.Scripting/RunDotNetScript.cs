using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Scripting.RunDotNetScriptBase;
using FarnahadFlowFusion.Service.Scripting.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace FarnahadFlowFusion.Action.Scripting;

public class RunDotNetScript : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Run .NET script";

    public Language Language { get; set; }
    public ActionInput DotNetScriptImports { get; set; }
    public ActionInput ReferencesToBeLoaded { get; set; }
    public List<ScriptParameter> ScriptParameters { get; set; }
    public ActionInput DotNetCodeToRun { get; set; }

    public RunDotNetScript()
    {
        _cSharpService = new CSharpService();

        Language = Language.CSharp;
        DotNetScriptImports = new ActionInput();
        ReferencesToBeLoaded = new ActionInput();
        ScriptParameters = new List<ScriptParameter>();
        DotNetCodeToRun = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var dotNetScriptImportsValue = await _cSharpService.EvaluateActionInput<string>(sandBox, DotNetScriptImports);
        var referencesToBeLoadedValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ReferencesToBeLoaded);
        var dDotNetCodeToRunValue = await _cSharpService.EvaluateActionInput<string>(sandBox, DotNetCodeToRun);

        // Language

        _cSharpService.Evaluate()
        var references = referencesToBeLoadedValue.Split(',').Select(x => x.Trim()).ToList() ?? new List<string>();
        var options = ScriptOptions.Default.WithReferences(references).WithImports(dotNetScriptImportsValue.Split(',').Select(x => x.Trim()));

        try
        {
            var result = await CSharpScript.EvaluateAsync<object>(DotNetCodeToRun.Value, options);
            Console.WriteLine($"Script Result: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }



        XXXXXXXXXXXX.Value = new Random().Next(XXXXXXXXXXXX, XXXXXXXXXXXX);

        sandBox.Variables.Add(XXXXXXXXXXXX);
    }
}