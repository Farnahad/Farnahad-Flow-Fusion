using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.MessageBox;

public class DisplaySelectFromListDialog : IAction
{
    private readonly CSharpService _cSharpService;

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
        _cSharpService = new CSharpService();

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
        var dialogTitleValue = await _cSharpService.EvaluateActionInput<string>(sandBox, DialogTitle);
        var dialogMessageValue = await _cSharpService.EvaluateActionInput<string>(sandBox, DialogMessage);
        var listChooseFromValue = await _cSharpService.EvaluateActionInput<List<string>>(sandBox, ListChooseFrom);

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

        sandBox.Variables.Add(SelectedIndex);
        sandBox.Variables.Add(ButtonPressed);
    }
}