using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Action.MessageBox.DisplayInputDialogBase;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.MessageBox;

public class DisplayInputDialog : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Display input dialog";

    public ActionInput InputDialogTitle { get; set; }
    public ActionInput InputDialogMessage { get; set; }
    public ActionInput DefaultValue { get; set; }
    public InputType InputType { get; set; }
    public bool KeepInputDialogAlwaysOnTop { get; set; }
    public Variable UerInput { get; set; }
    public Variable ButtonPressed { get; set; }

    public DisplayInputDialog()
    {
        _cSharpService = new CSharpService();

        InputDialogTitle = new ActionInput();
        InputDialogMessage = new ActionInput();
        DefaultValue = new ActionInput();
        InputType = InputType.SingleLine;
        KeepInputDialogAlwaysOnTop = false;
        UerInput = new Variable();
        ButtonPressed = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var inputDialogTitleValue = await _cSharpService.EvaluateActionInput<string>(sandBox, InputDialogTitle);
        var inputDialogMessageValue = await _cSharpService.EvaluateActionInput<string>(sandBox, InputDialogMessage);
        var defaultValueValue = await _cSharpService.EvaluateActionInput<string>(sandBox, DefaultValue);

        switch (InputType)
        {
            case InputType.Multiline:
                break;
            case InputType.Password:
                break;
            case InputType.SingleLine:
                break;
        }

        if (KeepInputDialogAlwaysOnTop)
        {

        }



        UerInput.Value = "UerInput";
        ButtonPressed.Value = "ButtonPressed";

        sandBox.Variables.Add(UerInput);
        sandBox.Variables.Add(ButtonPressed);
    }
}