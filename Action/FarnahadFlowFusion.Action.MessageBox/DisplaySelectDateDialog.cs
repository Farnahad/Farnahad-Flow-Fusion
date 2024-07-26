using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.MessageBox.DisplaySelectDateDialogBase;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.MessageBox;

public class DisplaySelectDateDialog : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Display select date dialog";

    public ActionInput DialogTitle { get; set; }
    public ActionInput DialogMessage { get; set; }
    public DialogType DialogType { get; set; }
    public PromptFor PromptFor { get; set; }
    public ActionInput DefaultValue { get; set; }
    public bool KeepDateSelectionDialogAlwaysOnTop { get; set; }
    public Variable SelectedDate { get; set; }
    public Variable ButtonPressed { get; set; }

    public DisplaySelectDateDialog()
    {
        _cSharpService = new CSharpService();

        DialogTitle = new ActionInput();
        DialogMessage = new ActionInput();
        DialogType = DialogType.SingleDate;
        PromptFor = PromptFor.DateOnly;
        DefaultValue = new ActionInput();
        KeepDateSelectionDialogAlwaysOnTop = false;
        SelectedDate = new Variable();
        ButtonPressed = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var dialogTitleValue = await _cSharpService.EvaluateActionInput<string>(sandBox, DialogTitle);
        var dialogMessageValue = await _cSharpService.EvaluateActionInput<string>(sandBox, DialogMessage);
        var defaultValueValue = await _cSharpService.EvaluateActionInput<string>(sandBox, DefaultValue);

        switch (DialogType)
        {
            case DialogType.DateRange2Dates:
                break;
            case DialogType.SingleDate:
                break;
        }

        switch (PromptFor)
        {
            case PromptFor.DateAndTime:
                break;
            case PromptFor.DateOnly:
                break;
        }

        if (KeepDateSelectionDialogAlwaysOnTop)
        {

        }


        SelectedDate.Value = "SelectedDate";
        ButtonPressed.Value = "ButtonPressed";

        sandBox.Variables.Add(SelectedDate);
        sandBox.Variables.Add(ButtonPressed);
    }
}