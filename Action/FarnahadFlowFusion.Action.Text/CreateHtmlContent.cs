using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Text;

public class CreateHtmlContent : IAction
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