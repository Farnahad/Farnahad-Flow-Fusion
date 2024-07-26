using FarnahadFlowFusion.Action.Main;
using Scriban;
using Scriban.Syntax;

namespace FarnahadFlowFusion.Service.Main.TemplateEngine;

public class TemplateEngineService
{
    public string GenerateFormula(SandBox sandBox, string formula)
    {
        var model = new Dictionary<string, object>();
        foreach (var variable in sandBox.Variables)
            model.Add(variable.Name, variable.Name);

        var processedTemplateText = PreProcessTemplate(formula);


        var template = Template.Parse(processedTemplateText);
        var variables = ExtractVariables(template);

        if (variables.Any())
            return template.Render(model);

        return formula;
    }

    static HashSet<string> ExtractVariables(Template template)
    {
        var visitor = new VariableVisitor();
        visitor.Visit(template.Page);
        return visitor.Variables;
    }

    private string PreProcessTemplate(string templateText)
    {
        return System.Text.RegularExpressions.Regex.Replace(templateText, @"%(\w+)%", @"{{$1}}");
    }

    public class VariableVisitor : ScriptVisitor
    {
        public HashSet<string> Variables { get; } = new HashSet<string>();

        public override void Visit(ScriptVariableGlobal node)
        {
            Variables.Add(node.Name);
            base.Visit(node);
        }
    }
}