using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.MessageBox;

public class DisplaySelectFileDialog : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Display select file dialog";

    public ActionInput DialogTitle { get; set; }
    public ActionInput InitialFolder { get; set; }
    public ActionInput FileFilter { get; set; }
    public bool KeepFileSelectionDialogAlwaysOnTop { get; set; }
    public bool AllowMultipleSelection { get; set; }
    public bool CheckIfFileExists { get; set; }
    public Variable SelectedFile { get; set; }
    public Variable ButtonPressed { get; set; }

    public DisplaySelectFileDialog()
    {
        _cSharpService = new CSharpService();

        DialogTitle = new ActionInput();
        InitialFolder = new ActionInput();
        FileFilter = new ActionInput();
        KeepFileSelectionDialogAlwaysOnTop = false;
        AllowMultipleSelection = false;
        CheckIfFileExists = false;
        SelectedFile = new Variable();
        ButtonPressed = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var dialogTitleValue = await _cSharpService.EvaluateActionInput<string>(sandBox, DialogTitle);
        var initialFolderValue = await _cSharpService.EvaluateActionInput<string>(sandBox, InitialFolder);
        var fileFilterValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FileFilter);

        var openFileDialog = new OpenFileDialog
        {
            Title = dialogTitleValue ?? "Select a File",
            InitialDirectory = initialFolderValue ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            Filter = fileFilterValue,
            Multiselect = AllowMultipleSelection,
            CheckFileExists = CheckIfFileExists
        };

        if (KeepFileSelectionDialogAlwaysOnTop)
        {
        }

        if (openFileDialog.ShowDialog() ?? false)
        {
            SelectedFile.Value = openFileDialog.FileName;
            ButtonPressed.Value = "OK";

        }
        else
        {
            SelectedFile.Value = null;
            ButtonPressed.Value = "Cancel";
        }

        sandBox.Variables.Add(SelectedFile);
        sandBox.Variables.Add(ButtonPressed);
    }
}