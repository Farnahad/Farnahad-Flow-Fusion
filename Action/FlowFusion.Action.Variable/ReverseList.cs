﻿using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.Variable;

public class ReverseList : IAction
{
    public string Name => "Reverse List";

    public ActionInput ListToReverse { get; set; }

    public ReverseList()
    {
        ListToReverse = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var listToReverse = await sandBox.EvaluateActionInput<List<object>>(ListToReverse);

        listToReverse.Reverse();
    }
}