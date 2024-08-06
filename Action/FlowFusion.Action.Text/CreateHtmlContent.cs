using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Text;

public class CreateHtmlContent : IAction //XXXXXXXXXXXX
{
    public string Name => "Create HTML content";

    public ActionInput Html { get; set; }
    public Variable HtmlContent { get; set; }

    public CreateHtmlContent()
    {
        Html = new ActionInput();
        HtmlContent = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var htmlValue = await sandBox.EvaluateActionInput<string>(Html);

        HtmlContent.Value = htmlValue;

        sandBox.SetVariable(HtmlContent);
    }
}