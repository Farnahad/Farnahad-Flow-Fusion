﻿using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.File;

public class ConvertFileToBase64 : IAction
{
    public string Name => "Convert file to Base64";

    public ActionInput FilePath { get; set; }
    public Variable Base64Text { get; set; }

    public ConvertFileToBase64()
    {
        FilePath = new ActionInput();
        Base64Text = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);
        Base64Text.Value = Convert.ToBase64String(await global::System.IO.File.ReadAllBytesAsync(filePathValue));

        sandBox.SetVariable(Base64Text);
    }
}