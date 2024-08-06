using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.MessageBox;

public class DisplaySelectFolderDialog : IAction //XXXXXXXXXXXX
{
    public string Name => "Display select folder dialog";

    public ActionInput DialogDescription { get; set; }
    public ActionInput InitialFolder { get; set; }
    public bool KeepFolderSelectionDialogAlwaysOnTop { get; set; }
    public Variable SelectedFolder { get; set; }
    public Variable ButtonPressed { get; set; }

    public DisplaySelectFolderDialog()
    {
        DialogDescription = new ActionInput();
        InitialFolder = new ActionInput();
        KeepFolderSelectionDialogAlwaysOnTop = false;
        SelectedFolder = new Variable();
        ButtonPressed = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var dialogDescriptionValue = await sandBox.EvaluateActionInput<string>(DialogDescription);
        var initialFolderValue = await sandBox.EvaluateActionInput<string>(InitialFolder);


        using (var folderBrowserDialog = new FolderBrowserDialog())
        {
            folderBrowserDialog.Description = dialogDescriptionValue ?? "Select a Folder";
            folderBrowserDialog.SelectedPath = initialFolderValue ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (KeepFolderSelectionDialogAlwaysOnTop)
                folderBrowserDialog.ShowNewFolderButton = true;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedFolder.Value = folderBrowserDialog.SelectedPath;
                ButtonPressed.Value = "OK";
            }
            else
            {
                ButtonPressed.Value = "Cancel";
                SelectedFolder.Value = null;
            }
        }

        sandBox.SetVariable(ButtonPressed);
        sandBox.SetVariable(SelectedFolder);
    }
}