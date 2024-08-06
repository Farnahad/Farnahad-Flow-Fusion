using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Action.MessageBox.DisplaySelectDateDialogBase;

namespace FlowFusion.Action.MessageBox;

public class DisplaySelectDateDialog : IAction //XXXXXXXXXXXX
{
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
        var dialogTitleValue = await sandBox.EvaluateActionInput<string>(DialogTitle);
        var dialogMessageValue = await sandBox.EvaluateActionInput<string>(DialogMessage);
        var defaultValueValue = await sandBox.EvaluateActionInput<string>(DefaultValue);

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

        sandBox.SetVariable(SelectedDate);
        sandBox.SetVariable(ButtonPressed);
    }
}