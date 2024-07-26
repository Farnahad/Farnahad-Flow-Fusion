using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.Text;

public class CreateHtmlContent : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Create HTML content";

    public ActionInput Html { get; set; }
    public Variable HtmlContent { get; set; }

    public CreateHtmlContent()
    {
        _cSharpService = new CSharpService();

        Html = new ActionInput();
        HtmlContent = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var htmlValue = await _cSharpService.EvaluateActionInput<string>(sandBox, Html);

        HtmlContent.Value = htmlValue;

        sandBox.Variables.Add(HtmlContent);
    }
}