using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.MessageBox;

public class DisplaySelectFromListDialog : IAction
{
    public string Name => "Display select from list dialog";

    public ActionInput DialogTitle { get; set; }
    public ActionInput DialogMessage { get; set; }
    public ActionInput ListChooseFrom { get; set; }
    public bool KeepSelectDialogAlwaysOnTop { get; set; }
    public bool LimitToList { get; set; }
    public bool AllowEmptySelection { get; set; }
    public bool AllowMultipleSelection { get; set; }
    public Variable SelectedItem { get; set; }
    public Variable SelectedIndex { get; set; }
    public Variable ButtonPressed { get; set; }

    public DisplaySelectFromListDialog()
    {
        DialogTitle = new ActionInput();
        DialogMessage = new ActionInput();
        ListChooseFrom = new ActionInput();
        KeepSelectDialogAlwaysOnTop = false;
        LimitToList = true;
        AllowEmptySelection = false;
        AllowMultipleSelection = false;
        SelectedItem = new Variable();
        SelectedIndex = new Variable();
        ButtonPressed = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var dialogTitleValue = await sandBox.EvaluateActionInput<string>(DialogTitle);
        var dialogMessageValue = await sandBox.EvaluateActionInput<string>(DialogMessage);
        var listChooseFromValue = await sandBox.EvaluateActionInput<List<string>>(ListChooseFrom);

        if (KeepSelectDialogAlwaysOnTop)
        {

        }

        if (LimitToList)
        {

        }

        if (AllowEmptySelection)
        {

        }

        if (AllowMultipleSelection)
        {

        }


        SelectedIndex.Value = "SelectedIndex";
        ButtonPressed.Value = "ButtonPressed";

        sandBox.SetVariable(SelectedIndex);
        sandBox.SetVariable(ButtonPressed);
    }
}