﻿using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.File;

public class ConvertFileToBinaryData : IAction
{
    public string Name => "Convert file to binary data";

    public ActionInput FilePath { get; set; }
    public Variable BinaryData { get; set; }

    public ConvertFileToBinaryData()
    {
        FilePath = new ActionInput();
        BinaryData = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var filePathValue = await sandBox.EvaluateActionInput<string>(FilePath);

        BinaryData.Value = await global::System.IO.File.ReadAllBytesAsync(filePathValue);

        sandBox.SetVariable(BinaryData);
    }
}