using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using Microsoft.Win32;

namespace FlowFusion.Action.MessageBox;

public class DisplaySelectFileDialog : IAction //XXXXXXXXXXXX
{
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
        var dialogTitleValue = await sandBox.EvaluateActionInput<string>(DialogTitle);
        var initialFolderValue = await sandBox.EvaluateActionInput<string>(InitialFolder);
        var fileFilterValue = await sandBox.EvaluateActionInput<string>(FileFilter);

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

        sandBox.SetVariable(SelectedFile);
        sandBox.SetVariable(ButtonPressed);
    }
}