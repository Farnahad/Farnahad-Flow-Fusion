using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Action.Text.TrimTextBase;

namespace FarnahadFlowFusion.Action.Text;

public class TrimText : IAction
{
    public string Name => "Trim text";

    public ActionInput TextToTrim { get; set; }
    public WhatToTrim WhatToTrim { get; set; }
    public Variable TrimmedText { get; set; }

    public TrimText()
    {
        TextToTrim = new ActionInput();
        WhatToTrim = WhatToTrim.WhitespaceCharactersFromTheBeginningAndEnd;
        TrimmedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToTrimValue = await sandBox.EvaluateActionInput<string>(TextToTrim);

        switch (WhatToTrim)
        {
            case WhatToTrim.WhitespaceCharactersFromTheBeginning:
                TrimmedText.Value = textToTrimValue.TrimStart();
                break;
            case WhatToTrim.WhitespaceCharactersFromTheBeginningAndEnd:
                TrimmedText.Value = textToTrimValue.Trim();
                break;
            case WhatToTrim.WhitespaceCharactersFromTheEnd:
                TrimmedText.Value = textToTrimValue.TrimEnd();
                break;
        }

        sandBox.SetVariable(TrimmedText);
    }
}