using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Text.CropTextBase;

namespace FarnahadFlowFusion.Action.Text;

public class CropText : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Crop text";

    public ActionInput OriginalText { get; set; }
    public Mode Mode { get; set; }
    public ActionInput EndFlag { get; set; }
    public bool IgnoreCase { get; set; }
    public Variable CroppedText { get; set; }
    public Variable IsFlagFound { get; set; }

    public CropText()
    {
        _cSharpService = new CSharpService();

        OriginalText = new ActionInput();
        Mode = Mode.GetTextBeforeTheSpecifiedFlag;
        EndFlag = new ActionInput();
        IgnoreCase = false;
        CroppedText = new Variable();
        CroppedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var originalTextValue = await _cSharpService.EvaluateActionInput<string>(sandBox, OriginalText);
        var endFlagValue = await _cSharpService.EvaluateActionInput<string>(sandBox, EndFlag);


        var comparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        int flagIndex = originalTextValue.IndexOf(endFlagValue, comparison);

        var croppedText = originalTextValue;
        var isFlagFound = false;

        if (flagIndex == -1)
        {
            croppedText = originalTextValue;
            isFlagFound = false;
        }
        else
        {
            switch (Mode)
            {
                case Mode.GetTextAfterTheSpecifiedFlag:
                    croppedText = originalTextValue.Substring(flagIndex + endFlagValue.Length);
                    isFlagFound = true;
                    break;
                case Mode.GetTextBeforeTheSpecifiedFlag:
                    croppedText = originalTextValue.Substring(0, flagIndex);
                    isFlagFound = true;
                    break;
                case Mode.GetTextBetweenTheTwoSpecifiedFlags:
                    if (string.IsNullOrEmpty(endFlagValue) == false)
                    {
                        int endFlagIndex = originalTextValue.IndexOf(endFlagValue, flagIndex + endFlagValue.Length, comparison);

                        if (endFlagIndex == -1)
                        {
                            croppedText = originalTextValue;
                            isFlagFound = false;
                        }
                        else
                        {
                            croppedText = originalTextValue.Substring(flagIndex + endFlagValue.Length,
                                endFlagIndex - (flagIndex + endFlagValue.Length));
                            isFlagFound = true;
                        }
                    }
                    break;
            }
        }

        CroppedText.Value = croppedText;
        IsFlagFound.Value = isFlagFound;

        sandBox.Variables.Add(CroppedText);
        sandBox.Variables.Add(IsFlagFound);
    }
}