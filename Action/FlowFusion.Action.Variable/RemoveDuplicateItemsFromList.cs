﻿using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.Variable;

public class RemoveDuplicateItemsFromList : IAction
{
    public string Name => "Remove Duplicate Items From List";

    public ActionInput ListToRemoveDuplicateItemsFrom { get; set; }
    public bool IgnoreTextCaseWhileSearchingForDuplicateItems { get; set; }

    public RemoveDuplicateItemsFromList()
    {
        ListToRemoveDuplicateItemsFrom = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var listToRemoveDuplicateItemsFrom = await sandBox.EvaluateActionInput<List<object>>(ListToRemoveDuplicateItemsFrom);

        listToRemoveDuplicateItemsFrom = listToRemoveDuplicateItemsFrom
            .GroupBy(item => item).Select(g => g.First()).ToList();
    }
}