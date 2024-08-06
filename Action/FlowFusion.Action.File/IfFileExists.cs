using FlowFusion.Action.File.IfFileExistsBase;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.File;

public class IfFileExists : IAction
{
    public string Name => "If File Exists";

    public IfFile IfFile { get; set; }
    public ActionInput FilePath { get; set; }
    public List<IAction> Actions { get; set; }

    public IfFileExists()
    {
        IfFile = IfFile.Exists;
        FilePath = new ActionInput();
        Actions = new List<IAction>();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filePath = await sandBox.EvaluateActionInput<string>(FilePath);

        var result = false;

        if (IfFile == IfFile.DoesNotExist)
        {
            result = global::System.IO.File.Exists(filePath);
        }
        else if (IfFile == IfFile.Exists)
        {
            result = global::System.IO.File.Exists(filePath) == false;
        }

        if (result)
        {
            foreach (var action in Actions)
                await action.Execute(sandBox);
        }
    }
}