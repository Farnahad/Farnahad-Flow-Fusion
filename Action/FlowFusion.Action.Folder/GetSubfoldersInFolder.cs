﻿using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Folder;

public class GetSubfoldersInFolder : IAction
{
    public string Name => "Get subfolders in folder";

    public ActionInput Folder { get; set; }
    public ActionInput FolderFilter { get; set; }
    public bool IncludeSubfolders { get; set; }
    public Variable Folders { get; set; }

    public GetSubfoldersInFolder()
    {
        Folder = new ActionInput();
        FolderFilter = new ActionInput();
        IncludeSubfolders = false;
        Folders = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var folderValue = await sandBox.EvaluateActionInput<string>(Folder);
        var folderFilterValue = await sandBox.EvaluateActionInput<string>(FolderFilter);

        var filter = string.IsNullOrWhiteSpace(folderFilterValue) == false ? folderFilterValue : "*";
        var searchOption = IncludeSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        Folders.Value = new List<string>(Directory.GetDirectories(folderValue, filter, searchOption));

        sandBox.SetVariable(Folders);
    }
}