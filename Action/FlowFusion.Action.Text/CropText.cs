using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;
using FlowFusion.Service.Text.Text.Base;

namespace FlowFusion.Action.Text;

public class CropText : IAction
{
    private readonly ITextService _textService;

    public string Name => "Crop text";

    public ActionInput OriginalText { get; set; }
    public CropMode Mode { get; set; }
    public ActionInput EndFlag { get; set; }
    public bool IgnoreCase { get; set; }
    public Variable CroppedText { get; set; }
    public Variable IsFlagFound { get; set; }

    public CropText(ITextService textService)
    {
        _textService = textService;
        OriginalText = new ActionInput();
        Mode = CropMode.GetTextBeforeTheSpecifiedFlag;
        EndFlag = new ActionInput();
        IgnoreCase = false;
        CroppedText = new Variable();
        CroppedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var originalTextValue = await sandBox.EvaluateActionInput<string>(OriginalText);
        var endFlagValue = await sandBox.EvaluateActionInput<string>(EndFlag);

        (CroppedText.Value, IsFlagFound.Value) = _textService.CropText(originalTextValue, Mode, endFlagValue, IgnoreCase);

        sandBox.SetVariable(CroppedText);
        sandBox.SetVariable(IsFlagFound);
    }
}