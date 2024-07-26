using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Text.TrimTextBase;

namespace FarnahadFlowFusion.Action.Text;

public class TrimText : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Trim text";

    public ActionInput TextToTrim { get; set; }
    public WhatToTrim WhatToTrim { get; set; }
    public Variable TrimmedText { get; set; }

    public TrimText()
    {
        _cSharpService = new CSharpService();

        TextToTrim = new ActionInput();
        WhatToTrim = WhatToTrim.WhitespaceCharactersFromTheBeginningAndEnd;
        TrimmedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToTrimValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToTrim);

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

        sandBox.Variables.Add(TrimmedText);
    }
}