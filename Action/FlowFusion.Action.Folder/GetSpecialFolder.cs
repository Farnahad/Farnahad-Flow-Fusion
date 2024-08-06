using FlowFusion.Action.Folder.GetSpecialFolderBase;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Folder;

public class GetSpecialFolder : IAction //XXXXXXXXXXXX
{
    public string Name => "Get special folder";

    public SpecialFolderName SpecialFolderName { get; set; }
    public ActionInput SpecialFolderPath { get; set; }
    public Variable SpecialFolderPathVariable { get; set; }

    public GetSpecialFolder()
    {
        SpecialFolderName = SpecialFolderName.CommonProgramFiles;
        SpecialFolderPath = new ActionInput();
        SpecialFolderPathVariable = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var specialFolderPathValue = await sandBox.EvaluateActionInput<string>(SpecialFolderPath);

        var path = "";

        switch (SpecialFolderName)
        {
            case SpecialFolderName.ApplicationData:
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                break;
            case SpecialFolderName.CommonApplicationData:
                path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                break;
            case SpecialFolderName.CommonProgramFiles:
                path = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
                break;
            case SpecialFolderName.Cookies:
                path = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
                break;
            case SpecialFolderName.Desktop:
                path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                break;
            case SpecialFolderName.Documents:
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                break;
            case SpecialFolderName.Favorites:
                path = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
                break;
            case SpecialFolderName.History:
                path = Environment.GetFolderPath(Environment.SpecialFolder.History);
                break;
            case SpecialFolderName.InternetCache:
                path = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                break;
            case SpecialFolderName.LocalApplicationData:
                path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                break;
            case SpecialFolderName.Music:
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                break;
            case SpecialFolderName.Pictures:
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                break;
            case SpecialFolderName.ProgramFiles:
                path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                break;
            case SpecialFolderName.Programs:
                path = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
                break;
            case SpecialFolderName.Recent:
                path = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
                break;
            case SpecialFolderName.SendTo:
                path = Environment.GetFolderPath(Environment.SpecialFolder.SendTo);
                break;
            case SpecialFolderName.StartMenu:
                path = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
                break;
            case SpecialFolderName.Startup:
                path = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                break;
            case SpecialFolderName.System:
                path = Environment.GetFolderPath(Environment.SpecialFolder.System);
                break;
            case SpecialFolderName.Templates:
                path = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
                break;
        }

        if (string.IsNullOrEmpty(specialFolderPathValue) == false)
            path = specialFolderPathValue;

        SpecialFolderPathVariable.Value = path;

        sandBox.SetVariable(SpecialFolderPathVariable);
    }
}