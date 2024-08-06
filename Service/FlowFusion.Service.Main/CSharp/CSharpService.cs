using System.Text;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Service.Main.TemplateEngine;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace FarnahadFlowFusion.Service.Main.CSharp;

public class CSharpService
{
    private readonly TemplateEngineService _templateEngineService;

    public CSharpService()
    {
        _templateEngineService = new TemplateEngineService();
    }


    public async Task<object> Evaluate(string code)
    {
        try
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.IsDynamic && !string.IsNullOrWhiteSpace(assembly.Location))
                .Select(assembly => MetadataReference.CreateFromFile(assembly.Location));

            var state = await CSharpScript.RunAsync<int>("int answer = 42;");
            foreach (var variable in state.Variables)
                Console.WriteLine($"{variable.Name} = {variable.Value} of type {variable.Type}");


            return await CSharpScript.EvaluateAsync<object>(code, ScriptOptions.Default.
                .WithReferences(assemblies));

            //.WithImports("System", "System.Linq", "System.Collections.Generic")
        }
        catch (Exception e)
        {
            // ignored
        }

        return null;
    }

    public async Task<object> Evaluate(string code, SandBox sandBox)
    {
        try
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.IsDynamic && !string.IsNullOrWhiteSpace(assembly.Location))
                .Select(assembly => MetadataReference.CreateFromFile(assembly.Location));



            return await CSharpScript.EvaluateAsync<object>(code, ScriptOptions.Default
                .WithReferences(assemblies), sandBox, typeof(SandBox));

            //.WithImports("System", "System.Linq", "System.Collections.Generic")
        }
        catch (Exception e)
        {
            // ignored
        }

        return null;
    }

    public async Task<T> EvaluateActionInput<T>(SandBox sandBox, ActionInput actionInput)
    {
        var codeStringBuilder = new StringBuilder();
        codeStringBuilder.AppendLine("using System;");
        codeStringBuilder.AppendLine("using System.Collections.Generic;");
        codeStringBuilder.AppendLine();

        foreach (var variable in sandBox.Variables)
        {
            var type = variable.Value.GetType();

            if (type == typeof(bool))
            {
                codeStringBuilder.AppendLine($"bool {variable.Name} = (bool)GetVariableValue(\"{variable.Name}\");");
            }
            else if (type == typeof(double))
            {
                codeStringBuilder.AppendLine($"double {variable.Name} = (double)GetVariableValue(\"{variable.Name}\");");

            }
            else if (type == typeof(int))
            {
                codeStringBuilder.AppendLine($"int {variable.Name} = (int)GetVariableValue(\"{variable.Name}\");");

            }
            else if (type == typeof(string))
            {
                codeStringBuilder.AppendLine($"string {variable.Name} = (string)GetVariableValue(\"{variable.Name}\");");
            }
            else if (type == typeof(List<object>))
            {
                codeStringBuilder.AppendLine($"List<object> {variable.Name} = (List<object>)GetVariableValue(\"{variable.Name}\");");
            }
        }

        codeStringBuilder.AppendLine();
        codeStringBuilder.AppendLine(
            $"return {_templateEngineService.GenerateFormula(sandBox, actionInput.Formula)};");

        return (T)await Evaluate(codeStringBuilder.ToString(), sandBox);
    }
}