﻿using System.Diagnostics;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.CmdSession;

public class CloseCmdSession : IAction
{
    public string Name => "Close CMD session";

    public ActionInput CmdSession { get; set; }

    public CloseCmdSession()
    {
        CmdSession = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var cmdSessionValue = await sandBox.EvaluateActionInput<Process>(CmdSession);

        cmdSessionValue.Kill(true);
    }
}