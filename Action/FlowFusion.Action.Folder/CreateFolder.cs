﻿using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Folder;

public class CreateFolder : IAction
{
    public string Name => "Create folder";

    public ActionInput CreateNewFolderInto { get; set; }
    public ActionInput NewFolderName { get; set; }
    public Variable NewFolder { get; set; }

    public CreateFolder()
    {
        CreateNewFolderInto = new ActionInput();
        NewFolderName = new ActionInput();
        NewFolder = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var createNewFolderIntoValue = await sandBox.EvaluateActionInput<string>(CreateNewFolderInto);
        var newFolderNameValue = await sandBox.EvaluateActionInput<string>(NewFolderName);

        var directoryInfo = new DirectoryInfo(Path.Combine(createNewFolderIntoValue, newFolderNameValue));

        if (directoryInfo.Exists == false)
            directoryInfo.Create();

        NewFolder.Value = directoryInfo.FullName;

        sandBox.SetVariable(NewFolder);
    }
}