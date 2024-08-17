using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;
using FlowFusion.Service.Text.Text.Base;

namespace FlowFusion.Action.Text;

public class TrimText(ITextService textService) : IAction
{
    public string Name => "Trim text";

    public ActionInput TextToTrim { get; set; } = new();
    public WhatToTrim WhatToTrim { get; set; } = WhatToTrim.WhitespaceCharactersFromTheBeginningAndEnd;
    public Variable TrimmedText { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToTrimValue = await sandBox.EvaluateActionInput<string>(TextToTrim);

        TrimmedText.Value = textService.TrimText(textToTrimValue, WhatToTrim);

        sandBox.SetVariable(TrimmedText);
    }
}