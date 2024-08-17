using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;

namespace FlowFusion.Action.Text;

public class CreateHtmlContent(ITextService textService) : IAction
{
    public string Name => "Create HTML content";

    public ActionInput Html { get; set; } = new();
    public Variable HtmlContent { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var htmlValue = await sandBox.EvaluateActionInput<string>(Html);

        HtmlContent.Value = textService.CreateHtmlContent(htmlValue);

        sandBox.SetVariable(HtmlContent);
    }
}