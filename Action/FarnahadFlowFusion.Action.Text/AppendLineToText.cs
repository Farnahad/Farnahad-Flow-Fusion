using System.Text;
using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Text;

public class AppendLineToText : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Append line to text";

    public ActionInput OriginalText { get; set; }
    public ActionInput LineToAppend { get; set; }
    public Variable Result { get; set; }

    public AppendLineToText()
    {
        _cSharpService = new CSharpService();

        OriginalText = new ActionInput();
        LineToAppend = new ActionInput();
        Result = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var originalTextValue = await _cSharpService.EvaluateActionInput<string>(sandBox, OriginalText);
        var lineToAppendValue = await _cSharpService.EvaluateActionInput<string>(sandBox, LineToAppend);

        var stringBuilder = new StringBuilder(originalTextValue);
        stringBuilder.AppendLine(lineToAppendValue);
        Result.Value = stringBuilder.ToString();

        sandBox.Variables.Add(Result);
    }
}